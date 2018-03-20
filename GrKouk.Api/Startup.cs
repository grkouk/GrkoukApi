using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using GrKouk.Api.Data;
using GrKouk.Api.Dtos;
using GrKouk.Api.Models;
using GrKouk.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GrKouk.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                    });
                //options.AddPolicy("AllowSpecificOrigins",
                //    builder =>
                //    {
                //        builder.WithOrigins( "http://potos.tours",
                //            "http://thassos-rent-a-bike.com").AllowAnyMethod().AllowAnyHeader();
                //    });
            });

            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(options =>
                {
                    options.GeneralRules = new System.Collections.Generic.List<RateLimitRule>()
                    {
                        new RateLimitRule()
                        {

                            Endpoint = "*",
                            Limit = 30,
                            Period = "1m"
                        }
                    };
                    options.EnableEndpointRateLimiting = true;
                }
                );



            services.AddDbContext<ApiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();

            // Add application services.
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddTransient<IEmailSender, AuthMessageSender>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllOrigins");
            app.UseIpRateLimiting();

            //Configure Automapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Transaction, TransactionDto>()
                    .ForMember(dest => dest.TransactorName, opt => opt.MapFrom(src =>
                        src.Transactor.Name
                    ))
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                    .ForMember(dest=>dest.CompanyName, opt=>opt.MapFrom(src=>src.Company.Name))
                    .ForMember(dest => dest.CostCentreName, opt => opt.MapFrom(src => src.CostCentre.Name))
                    .ForMember(dest => dest.RevenueCentreName, opt => opt.MapFrom(src => src.RevenueCentre.Name))
                    .ForMember(dest => dest.AmountTotal,
                        opt => opt.ResolveUsing(src => src.AmountFpa + src.AmountNet));
                cfg.CreateMap<Transaction, TransactionCreateDto>().ReverseMap();
            });
            app.UseMvc();
        }
    }
}

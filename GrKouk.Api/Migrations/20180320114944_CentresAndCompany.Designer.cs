using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GrKouk.Api.Data;

namespace GrKouk.Api.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20180320114944_CentresAndCompany")]
    partial class CentresAndCompany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GrKouk.Api.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("Code");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("GrKouk.Api.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("GrKouk.Api.Models.CostCentre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("CostCentres");
                });

            modelBuilder.Entity("GrKouk.Api.Models.Facility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .HasMaxLength(15);

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("GrKouk.Api.Models.RevenueCentre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("RevenueCentres");
                });

            modelBuilder.Entity("GrKouk.Api.Models.RoomQuoteRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Adults");

                    b.Property<int>("Children");

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<string>("Notes")
                        .HasMaxLength(150);

                    b.Property<DateTime>("RequestDate");

                    b.Property<string>("RequesterEmail")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("RequesterName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("RoomQuoteRequests");
                });

            modelBuilder.Entity("GrKouk.Api.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AmountFpa");

                    b.Property<decimal>("AmountNet");

                    b.Property<int>("CategoryId");

                    b.Property<int>("CompanyId");

                    b.Property<int>("CostCentreId");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<int>("Kind");

                    b.Property<string>("ReferenceCode");

                    b.Property<int>("RevenueCentreId");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("TransactionDate");

                    b.Property<int>("TransactorId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CostCentreId");

                    b.HasIndex("RevenueCentreId");

                    b.HasIndex("TransactionDate");

                    b.HasIndex("TransactorId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("GrKouk.Api.Models.Transactor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<string>("City")
                        .HasMaxLength(50);

                    b.Property<string>("Code")
                        .HasMaxLength(15);

                    b.Property<string>("EMail")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<string>("PhoneFax")
                        .HasMaxLength(200);

                    b.Property<string>("PhoneMobile")
                        .HasMaxLength(200);

                    b.Property<string>("PhoneWork")
                        .HasMaxLength(200);

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int?>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Transactors");
                });

            modelBuilder.Entity("GrKouk.Api.Models.VehicleQuoteRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<DateTime>("RequestDate");

                    b.Property<string>("RequesterEmail")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("RequesterName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("VehicleQuoteRequests");
                });

            modelBuilder.Entity("GrKouk.Api.Models.Transaction", b =>
                {
                    b.HasOne("GrKouk.Api.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("GrKouk.Api.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("GrKouk.Api.Models.CostCentre", "CostCentre")
                        .WithMany()
                        .HasForeignKey("CostCentreId");

                    b.HasOne("GrKouk.Api.Models.RevenueCentre", "RevenueCentre")
                        .WithMany()
                        .HasForeignKey("RevenueCentreId");

                    b.HasOne("GrKouk.Api.Models.Transactor", "Transactor")
                        .WithMany()
                        .HasForeignKey("TransactorId");
                });
        }
    }
}

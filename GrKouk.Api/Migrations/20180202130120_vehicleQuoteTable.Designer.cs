using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GrKouk.Api.Data;

namespace GrKouk.Api.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20180202130120_vehicleQuoteTable")]
    partial class vehicleQuoteTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GrKouk.Api.Migrations
{
    public partial class CentresAndCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CostCentreId",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RevenueCentreId",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CostCentres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCentres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RevenueCentres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevenueCentres", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CompanyId",
                table: "Transactions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CostCentreId",
                table: "Transactions",
                column: "CostCentreId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RevenueCentreId",
                table: "Transactions",
                column: "RevenueCentreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Companies_CompanyId",
                table: "Transactions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CostCentres_CostCentreId",
                table: "Transactions",
                column: "CostCentreId",
                principalTable: "CostCentres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_RevenueCentres_RevenueCentreId",
                table: "Transactions",
                column: "RevenueCentreId",
                principalTable: "RevenueCentres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Companies_CompanyId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CostCentres_CostCentreId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_RevenueCentres_RevenueCentreId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "CostCentres");

            migrationBuilder.DropTable(
                name: "RevenueCentres");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CompanyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CostCentreId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RevenueCentreId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CostCentreId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RevenueCentreId",
                table: "Transactions");
        }
    }
}

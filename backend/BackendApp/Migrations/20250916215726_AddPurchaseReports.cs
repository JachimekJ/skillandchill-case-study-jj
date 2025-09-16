using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DistributorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quarter = table.Column<string>(type: "TEXT", nullable: false),
                    LastYearSales = table.Column<decimal>(type: "TEXT", nullable: false),
                    Purchases = table.Column<decimal>(type: "TEXT", nullable: false),
                    Budget = table.Column<decimal>(type: "TEXT", nullable: false),
                    ActualSales = table.Column<decimal>(type: "TEXT", nullable: false),
                    YearVsLastYear = table.Column<decimal>(type: "TEXT", nullable: false),
                    YearVsBudget = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPOS = table.Column<int>(type: "INTEGER", nullable: false),
                    NewOpenings = table.Column<int>(type: "INTEGER", nullable: false),
                    OpeningsTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseReports");
        }
    }
}

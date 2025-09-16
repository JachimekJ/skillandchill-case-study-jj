using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendApp.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DistributorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quarter = table.Column<string>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false),
                    Professional = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pharmacy = table.Column<decimal>(type: "TEXT", nullable: false),
                    EcommerceB2C = table.Column<decimal>(type: "TEXT", nullable: false),
                    EcommerceB2B = table.Column<decimal>(type: "TEXT", nullable: false),
                    ThirdParty = table.Column<decimal>(type: "TEXT", nullable: false),
                    Other = table.Column<decimal>(type: "TEXT", nullable: false),
                    NewClients = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    EurTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesReports");
        }
    }
}

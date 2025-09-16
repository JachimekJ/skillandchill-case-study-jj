using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFiles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MediaFiles",
                columns: new[] { "Id", "Date", "Name", "Size", "Type", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SKU123_main_1.jpg", 512, "image", "/media/SKU123_main_1.jpg" },
                    { 2, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SKU123_manual.pdf", 120, "pdf", "/media/SKU123_manual.pdf" },
                    { 3, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "spring_sale_SKU123_SKU222.jpg", 1024, "image", "/media/spring_sale_SKU123_SKU222.jpg" },
                    { 4, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "SKU222_ingredients.pdf", 90, "pdf", "/media/SKU222_ingredients.pdf" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaFiles");
        }
    }
}

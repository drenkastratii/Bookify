using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookify.Migrations
{
    /// <inheritdoc />
    public partial class CreatingSeedingBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Abby Muscles");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "FullName",
                value: "Nancy Hoover");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "FullName",
                value: "Billy Spark");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { 4, "Laura Phantom" },
                    { 5, "Ron Parker" },
                    { 6, "Julian Button" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "This is the Description", 15.0, "Cotton Candy" },
                    { 2, "This is the Description", 18.0, "Cotton Candy" },
                    { 3, "This is the Description", 22.0, "Fortune of Time" },
                    { 4, "This is the Description", 9.0, "Leaves and Wonders" },
                    { 5, "This is the Description", 11.0, "Rock in The Ocean" },
                    { 6, "This is the Description", 17.0, "Vanish in The Sunset" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Stephen King");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "FullName",
                value: "George Orwell");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "FullName",
                value: "Frank Herbert");
        }
    }
}

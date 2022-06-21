using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIMovies.Migrations
{
    public partial class fixdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7,
                column: "Title",
                value: "The Batman");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7,
                column: "Title",
                value: "Tee Batman");
        }
    }
}

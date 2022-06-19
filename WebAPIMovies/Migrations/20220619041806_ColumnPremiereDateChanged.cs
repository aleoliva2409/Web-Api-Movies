using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIMovies.Migrations
{
    public partial class ColumnPremiereDateChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PremiereDate",
                table: "Movies",
                newName: "ReleaseDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Movies",
                newName: "PremiereDate");
        }
    }
}

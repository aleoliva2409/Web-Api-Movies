using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIMovies.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    InTheaters = table.Column<bool>(type: "bit", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoviesActors",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    Character = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesActors", x => new { x.MovieId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_MoviesActors_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviesActors_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoviesGenders",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesGenders", x => new { x.MovieId, x.GenderId });
                    table.ForeignKey(
                        name: "FK_MoviesGenders_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviesGenders_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "DateOfBirth", "Name", "Photo" },
                values: new object[,]
                {
                    { 1, new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jim Carrey", null },
                    { 2, new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert Downey Jr.", null },
                    { 3, new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chris Evans", null },
                    { 4, new DateTime(1974, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christian Bale", null },
                    { 5, new DateTime(1986, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert Pattinson", null }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Adventure" },
                    { 2, "Animation" },
                    { 3, "Thriller" },
                    { 4, "Romance" },
                    { 5, "Action" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "InTheaters", "Poster", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, false, null, new DateTime(2005, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Batman Begins" },
                    { 2, true, null, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avengers: Endgame" },
                    { 3, false, null, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avengers: Infinity Wars" },
                    { 4, false, null, new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sonic the Hedgehog" },
                    { 5, false, null, new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emma" },
                    { 6, false, null, new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wonder Woman 1984" },
                    { 7, true, null, new DateTime(2022, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Batman" }
                });

            migrationBuilder.InsertData(
                table: "MoviesActors",
                columns: new[] { "ActorId", "MovieId", "Character", "Order" },
                values: new object[,]
                {
                    { 4, 1, "Bruce Wayne/Batman", 1 },
                    { 2, 2, "Tony Stark", 1 },
                    { 3, 2, "Steve Rogers", 2 },
                    { 2, 3, "Tony Stark", 1 },
                    { 3, 3, "Steve Rogers", 2 },
                    { 1, 4, "Dr. Ivo Robotnik", 1 },
                    { 5, 7, "Bruce Wayne/Batman", 1 }
                });

            migrationBuilder.InsertData(
                table: "MoviesGenders",
                columns: new[] { "GenderId", "MovieId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 5, 1 },
                    { 1, 2 },
                    { 3, 2 },
                    { 1, 3 },
                    { 3, 3 },
                    { 1, 4 },
                    { 3, 5 },
                    { 4, 5 },
                    { 1, 6 },
                    { 3, 6 },
                    { 3, 7 },
                    { 5, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesActors_ActorId",
                table: "MoviesActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesGenders_GenderId",
                table: "MoviesGenders",
                column: "GenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesActors");

            migrationBuilder.DropTable(
                name: "MoviesGenders");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}

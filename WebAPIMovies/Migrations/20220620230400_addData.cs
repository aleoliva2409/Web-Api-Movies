using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace WebAPIMovies.Migrations
{
    public partial class addData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "DateOfBirth", "Name", "Photo" },
                values: new object[,]
                {
                    { 1, new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert Downey Jr.", null },
                    { 2, new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chris Evans", null },
                    { 3, new DateTime(1983, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chris Hemsworth", null },
                    { 4, new DateTime(1967, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mark Ruffalo", null },
                    { 5, new DateTime(1984, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Scarlett Johansson", null },
                    { 6, new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jim Carrey", null },
                    { 7, new DateTime(1942, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harrison Ford", null },
                    { 8, new DateTime(1974, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christian Bale", null },
                    { 9, new DateTime(1986, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert Pattinson", null },
                    { 10, new DateTime(1985, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gal Gadot", null },
                    { 11, new DateTime(1996, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anya Taylor-Joy", null }
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-58.41108996980968 -34.602103219290726)"), "Hoyts Abasto" },
                    { 2, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-58.428837001956595 -34.61455955124352)"), "Cinemark Caballito" },
                    { 3, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-58.46240345934307 -34.62907013478838)"), "Atlas Flores" },
                    { 4, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-73.986227 40.730898)"), "Village East Cinema" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "Animation" },
                    { 4, "Romance" },
                    { 5, "Thriller" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "InTheaters", "Poster", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avengers: Endgame" },
                    { 2, false, null, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avengers: Infinity Wars" },
                    { 3, false, null, new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sonic the Hedgehog" },
                    { 4, false, null, new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emma" },
                    { 5, false, null, new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wonder Woman 1984" },
                    { 6, false, null, new DateTime(2005, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Batman Begins" },
                    { 7, true, null, new DateTime(2022, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tee Batman" },
                    { 8, false, null, new DateTime(1997, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Air Force One" },
                    { 9, false, null, new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thor: Love and Thunder" }
                });

            migrationBuilder.InsertData(
                table: "MoviesActors",
                columns: new[] { "ActorId", "MovieId", "Character", "Order" },
                values: new object[,]
                {
                    { 1, 1, "Tony Stark", 1 },
                    { 2, 1, "Steve Rogers", 2 },
                    { 3, 1, "Thor", 3 },
                    { 4, 1, "Bruce Banner", 4 },
                    { 5, 1, "Scarlett Johansson", 5 },
                    { 1, 2, "Tony Stark", 1 },
                    { 2, 2, "Steve Rogers", 2 },
                    { 3, 2, "Thor", 3 },
                    { 4, 2, "Bruce Banner", 4 },
                    { 5, 2, "Scarlett Johansson", 5 },
                    { 6, 3, "Dr. Ivo Robotnik", 1 },
                    { 11, 4, "Emma Woodhouse", 1 },
                    { 10, 5, "Wonder woman", 1 },
                    { 8, 6, "Bruce Wayne/Batman", 1 },
                    { 9, 7, "Bruce Wayne/Batman", 1 },
                    { 7, 8, "Indiana Jones", 1 },
                    { 3, 9, "Thor", 1 }
                });

            migrationBuilder.InsertData(
                table: "MoviesGenres",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 5, 1 },
                    { 2, 2 },
                    { 5, 2 },
                    { 2, 3 },
                    { 4, 4 },
                    { 5, 4 },
                    { 2, 5 },
                    { 5, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 5, 8 },
                    { 1, 9 },
                    { 2, 9 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 11, 4 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 9, 7 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 7, 8 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}

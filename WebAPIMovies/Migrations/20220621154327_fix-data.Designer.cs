﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using WebAPIMovies;

#nullable disable

namespace WebAPIMovies.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220621154327_fix-data")]
    partial class fixdata
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WebAPIMovies.Entities.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Robert Downey Jr."
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Chris Evans"
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1983, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Chris Hemsworth"
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(1967, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Mark Ruffalo"
                        },
                        new
                        {
                            Id = 5,
                            DateOfBirth = new DateTime(1984, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Scarlett Johansson"
                        },
                        new
                        {
                            Id = 6,
                            DateOfBirth = new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Jim Carrey"
                        },
                        new
                        {
                            Id = 7,
                            DateOfBirth = new DateTime(1942, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Harrison Ford"
                        },
                        new
                        {
                            Id = 8,
                            DateOfBirth = new DateTime(1974, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Christian Bale"
                        },
                        new
                        {
                            Id = 9,
                            DateOfBirth = new DateTime(1986, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Robert Pattinson"
                        },
                        new
                        {
                            Id = 10,
                            DateOfBirth = new DateTime(1985, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Gal Gadot"
                        },
                        new
                        {
                            Id = 11,
                            DateOfBirth = new DateTime(1996, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Anya Taylor-Joy"
                        });
                });

            modelBuilder.Entity("WebAPIMovies.Entities.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Point>("Location")
                        .HasColumnType("geography");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Cinemas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Location = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-58.41108996980968 -34.602103219290726)"),
                            Name = "Hoyts Abasto"
                        },
                        new
                        {
                            Id = 2,
                            Location = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-58.428837001956595 -34.61455955124352)"),
                            Name = "Cinemark Caballito"
                        },
                        new
                        {
                            Id = 3,
                            Location = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-58.46240345934307 -34.62907013478838)"),
                            Name = "Atlas Flores"
                        },
                        new
                        {
                            Id = 4,
                            Location = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-73.986227 40.730898)"),
                            Name = "Village East Cinema"
                        });
                });

            modelBuilder.Entity("WebAPIMovies.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Animation"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Thriller"
                        });
                });

            modelBuilder.Entity("WebAPIMovies.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("InTheaters")
                        .HasColumnType("bit");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InTheaters = true,
                            ReleaseDate = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Avengers: Endgame"
                        },
                        new
                        {
                            Id = 2,
                            InTheaters = false,
                            ReleaseDate = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Avengers: Infinity Wars"
                        },
                        new
                        {
                            Id = 3,
                            InTheaters = false,
                            ReleaseDate = new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sonic the Hedgehog"
                        },
                        new
                        {
                            Id = 4,
                            InTheaters = false,
                            ReleaseDate = new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Emma"
                        },
                        new
                        {
                            Id = 5,
                            InTheaters = false,
                            ReleaseDate = new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Wonder Woman 1984"
                        },
                        new
                        {
                            Id = 6,
                            InTheaters = false,
                            ReleaseDate = new DateTime(2005, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Batman Begins"
                        },
                        new
                        {
                            Id = 7,
                            InTheaters = true,
                            ReleaseDate = new DateTime(2022, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Batman"
                        },
                        new
                        {
                            Id = 8,
                            InTheaters = false,
                            ReleaseDate = new DateTime(1997, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Air Force One"
                        },
                        new
                        {
                            Id = 9,
                            InTheaters = false,
                            ReleaseDate = new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Thor: Love and Thunder"
                        });
                });

            modelBuilder.Entity("WebAPIMovies.Entities.MoviesActors", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<string>("Character")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("MoviesActors");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            ActorId = 1,
                            Character = "Tony Stark",
                            Order = 1
                        },
                        new
                        {
                            MovieId = 1,
                            ActorId = 2,
                            Character = "Steve Rogers",
                            Order = 2
                        },
                        new
                        {
                            MovieId = 1,
                            ActorId = 3,
                            Character = "Thor",
                            Order = 3
                        },
                        new
                        {
                            MovieId = 1,
                            ActorId = 4,
                            Character = "Bruce Banner",
                            Order = 4
                        },
                        new
                        {
                            MovieId = 1,
                            ActorId = 5,
                            Character = "Scarlett Johansson",
                            Order = 5
                        },
                        new
                        {
                            MovieId = 2,
                            ActorId = 1,
                            Character = "Tony Stark",
                            Order = 1
                        },
                        new
                        {
                            MovieId = 2,
                            ActorId = 2,
                            Character = "Steve Rogers",
                            Order = 2
                        },
                        new
                        {
                            MovieId = 2,
                            ActorId = 3,
                            Character = "Thor",
                            Order = 3
                        },
                        new
                        {
                            MovieId = 2,
                            ActorId = 4,
                            Character = "Bruce Banner",
                            Order = 4
                        },
                        new
                        {
                            MovieId = 2,
                            ActorId = 5,
                            Character = "Scarlett Johansson",
                            Order = 5
                        },
                        new
                        {
                            MovieId = 3,
                            ActorId = 6,
                            Character = "Dr. Ivo Robotnik",
                            Order = 1
                        },
                        new
                        {
                            MovieId = 6,
                            ActorId = 8,
                            Character = "Bruce Wayne/Batman",
                            Order = 1
                        },
                        new
                        {
                            MovieId = 7,
                            ActorId = 9,
                            Character = "Bruce Wayne/Batman",
                            Order = 1
                        },
                        new
                        {
                            MovieId = 8,
                            ActorId = 7,
                            Character = "Indiana Jones",
                            Order = 1
                        },
                        new
                        {
                            MovieId = 9,
                            ActorId = 3,
                            Character = "Thor",
                            Order = 1
                        },
                        new
                        {
                            MovieId = 5,
                            ActorId = 10,
                            Character = "Wonder woman",
                            Order = 1
                        },
                        new
                        {
                            MovieId = 4,
                            ActorId = 11,
                            Character = "Emma Woodhouse",
                            Order = 1
                        });
                });

            modelBuilder.Entity("WebAPIMovies.Entities.MoviesCinemas", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "CinemaId");

                    b.HasIndex("CinemaId");

                    b.ToTable("MoviesCinemas");
                });

            modelBuilder.Entity("WebAPIMovies.Entities.MoviesGenres", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MoviesGenres");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            GenreId = 5
                        },
                        new
                        {
                            MovieId = 1,
                            GenreId = 2
                        },
                        new
                        {
                            MovieId = 2,
                            GenreId = 5
                        },
                        new
                        {
                            MovieId = 2,
                            GenreId = 2
                        },
                        new
                        {
                            MovieId = 3,
                            GenreId = 2
                        },
                        new
                        {
                            MovieId = 4,
                            GenreId = 5
                        },
                        new
                        {
                            MovieId = 4,
                            GenreId = 4
                        },
                        new
                        {
                            MovieId = 5,
                            GenreId = 5
                        },
                        new
                        {
                            MovieId = 5,
                            GenreId = 2
                        },
                        new
                        {
                            MovieId = 6,
                            GenreId = 1
                        },
                        new
                        {
                            MovieId = 7,
                            GenreId = 1
                        },
                        new
                        {
                            MovieId = 8,
                            GenreId = 1
                        },
                        new
                        {
                            MovieId = 8,
                            GenreId = 5
                        },
                        new
                        {
                            MovieId = 9,
                            GenreId = 1
                        },
                        new
                        {
                            MovieId = 9,
                            GenreId = 2
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebAPIMovies.Entities.MoviesActors", b =>
                {
                    b.HasOne("WebAPIMovies.Entities.Actor", "Actor")
                        .WithMany("MoviesActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPIMovies.Entities.Movie", "Movie")
                        .WithMany("MoviesActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("WebAPIMovies.Entities.MoviesCinemas", b =>
                {
                    b.HasOne("WebAPIMovies.Entities.Cinema", "Cinema")
                        .WithMany("MoviesCinemas")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPIMovies.Entities.Movie", "Movie")
                        .WithMany("MoviesCinemas")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("WebAPIMovies.Entities.MoviesGenres", b =>
                {
                    b.HasOne("WebAPIMovies.Entities.Genre", "Genre")
                        .WithMany("MoviesGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPIMovies.Entities.Movie", "Movie")
                        .WithMany("MoviesGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("WebAPIMovies.Entities.Actor", b =>
                {
                    b.Navigation("MoviesActors");
                });

            modelBuilder.Entity("WebAPIMovies.Entities.Cinema", b =>
                {
                    b.Navigation("MoviesCinemas");
                });

            modelBuilder.Entity("WebAPIMovies.Entities.Genre", b =>
                {
                    b.Navigation("MoviesGenres");
                });

            modelBuilder.Entity("WebAPIMovies.Entities.Movie", b =>
                {
                    b.Navigation("MoviesActors");

                    b.Navigation("MoviesCinemas");

                    b.Navigation("MoviesGenres");
                });
#pragma warning restore 612, 618
        }
    }
}

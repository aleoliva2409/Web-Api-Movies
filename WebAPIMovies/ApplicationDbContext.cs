using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using WebAPIMovies.Entities;

namespace WebAPIMovies
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }
        public DbSet<MoviesCinemas> MoviesCinemas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MoviesGenres>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MoviesCinemas>()
                .HasKey(mc => new { mc.MovieId, mc.CinemaId });

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {

            var rolAdminId = "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d";
            var usuarioAdminId = "5673b8cf-12de-44f6-92ad-fae4a77932ad";

            var rolAdmin = new IdentityRole()
            {
                Id = rolAdminId,
                Name = "Admin",
                NormalizedName = "Admin"
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();

            var username = "dannyoliva47@gmail.com";

            var usuarioAdmin = new IdentityUser()
            {
                Id = usuarioAdminId,
                UserName = username,
                NormalizedUserName = username,
                Email = username,
                NormalizedEmail = username,
                PasswordHash = passwordHasher.HashPassword(null, "Lospibesdela12.")
            };

            //modelBuilder.Entity<IdentityUser>()
            //    .HasData(usuarioAdmin);

            //modelBuilder.Entity<IdentityRole>()
            //    .HasData(rolAdmin);

            //modelBuilder.Entity<IdentityUserClaim<string>>()
            //    .HasData(new IdentityUserClaim<string>()
            //    {
            //        Id = 1,
            //        ClaimType = ClaimTypes.Role,
            //        UserId = usuarioAdminId,
            //        ClaimValue = "Admin"
            //    });

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            modelBuilder.Entity<Cinema>()
               .HasData(new List<Cinema>
               {
                    new Cinema{Id = 1, Name = "Hoyts Abasto", Location = geometryFactory.CreatePoint(new Coordinate(-58.41108996980968, -34.602103219290726))},
                    new Cinema{Id = 2, Name = "Cinemark Caballito", Location = geometryFactory.CreatePoint(new Coordinate(-58.428837001956595, -34.61455955124352))},
                    new Cinema{Id = 3, Name = "Atlas Flores", Location = geometryFactory.CreatePoint(new Coordinate(-58.46240345934307, -34.62907013478838))},
                    new Cinema{Id = 4, Name = "Village East Cinema", Location = geometryFactory.CreatePoint(new Coordinate(-73.986227, 40.730898))}
               });

            var action = new Genre() { Id = 1, Name = "Action" };
            var adventure = new Genre() { Id = 2, Name = "Adventure" };
            var animation = new Genre() { Id = 3, Name = "Animation" };
            var romance = new Genre() { Id = 4, Name = "Romance" };
            var thriller = new Genre() { Id = 5, Name = "Thriller" };

            modelBuilder.Entity<Genre>()
                .HasData(new List<Genre>
                {
                    action,
                    adventure,
                    animation,
                    romance,
                    thriller
                });

            var robertDowney = new Actor() { Id = 1, Name = "Robert Downey Jr.", DateOfBirth = new DateTime(1965, 4, 4) };
            var chrisEvans = new Actor() { Id = 2, Name = "Chris Evans", DateOfBirth = new DateTime(1981, 06, 13) };
            var chrisHemsworth = new Actor() { Id = 3, Name = "Chris Hemsworth", DateOfBirth = new DateTime(1983, 08, 11) };
            var markRuffalo = new Actor() { Id = 4, Name = "Mark Ruffalo", DateOfBirth = new DateTime(1967, 11, 22) };
            var scarlettJohansson = new Actor() { Id = 5, Name = "Scarlett Johansson", DateOfBirth = new DateTime(1984, 11, 22) };
            var jimCarrey = new Actor() { Id = 6, Name = "Jim Carrey", DateOfBirth = new DateTime(1962, 01, 17) };
            var harrisonFord = new Actor() { Id = 7, Name = "Harrison Ford", DateOfBirth = new DateTime(1942, 07, 13) };
            var christianBale = new Actor() { Id = 8, Name = "Christian Bale", DateOfBirth = new DateTime(1974, 01, 30) };
            var robertPattinson = new Actor() { Id = 9, Name = "Robert Pattinson", DateOfBirth = new DateTime(1986, 05, 13) };
            var galGadot = new Actor() { Id = 10, Name = "Gal Gadot", DateOfBirth = new DateTime(1985, 04, 30) };
            var anyaTaylorJoy = new Actor() { Id = 11, Name = "Anya Taylor-Joy", DateOfBirth = new DateTime(1996, 04, 16) };

            modelBuilder.Entity<Actor>()
                .HasData(new List<Actor>
                {
                    robertDowney,
                    chrisEvans,
                    chrisHemsworth,
                    markRuffalo,
                    scarlettJohansson,
                    jimCarrey,
                    harrisonFord,
                    christianBale,
                    robertPattinson,
                    galGadot,
                    anyaTaylorJoy
                });

            var endgame = new Movie()
            {
                Id = 1,
                Title = "Avengers: Endgame",
                InTheaters = true,
                ReleaseDate = new DateTime(2019, 04, 26)
            };

            var iw = new Movie()
            {
                Id = 2,
                Title = "Avengers: Infinity Wars",
                InTheaters = false,
                ReleaseDate = new DateTime(2019, 04, 26)
            };

            var sonic = new Movie()
            {
                Id = 3,
                Title = "Sonic the Hedgehog",
                InTheaters = false,
                ReleaseDate = new DateTime(2020, 02, 28)
            };
            var emma = new Movie()
            {
                Id = 4,
                Title = "Emma",
                InTheaters = false,
                ReleaseDate = new DateTime(2020, 02, 21)
            };
            var wonderwoman = new Movie()
            {
                Id = 5,
                Title = "Wonder Woman 1984",
                InTheaters = false,
                ReleaseDate = new DateTime(2020, 08, 14)
            };
            var batmanBegins = new Movie()
            {
                Id = 6,
                Title = "Batman Begins",
                InTheaters = false,
                ReleaseDate = new DateTime(2005, 06, 16)
            };
            var theBatman = new Movie()
            {
                Id = 7,
                Title = "The Batman",
                InTheaters = true,
                ReleaseDate = new DateTime(2022, 03, 04)
            };
            var airForceOne = new Movie()
            {
                Id = 8,
                Title = "Air Force One",
                InTheaters = false,
                ReleaseDate = new DateTime(1997, 12, 04)
            };
            var thorLoveAndThunder = new Movie()
            {
                Id = 9,
                Title = "Thor: Love and Thunder",
                InTheaters = false,
                ReleaseDate = new DateTime(2022, 07, 07)
            };

            modelBuilder.Entity<Movie>()
                .HasData(new List<Movie>
                {
                    endgame,
                    iw,
                    sonic,
                    emma,
                    wonderwoman,
                    batmanBegins,
                    theBatman,
                    airForceOne,
                    thorLoveAndThunder
                });

            modelBuilder.Entity<MoviesGenres>().HasData(
                new List<MoviesGenres>()
                {
                    new MoviesGenres(){MovieId = endgame.Id, GenreId = thriller.Id},
                    new MoviesGenres(){MovieId = endgame.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = iw.Id, GenreId = thriller.Id},
                    new MoviesGenres(){MovieId = iw.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = sonic.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = emma.Id, GenreId = thriller.Id},
                    new MoviesGenres(){MovieId = emma.Id, GenreId = romance.Id},
                    new MoviesGenres(){MovieId = wonderwoman.Id, GenreId = thriller.Id},
                    new MoviesGenres(){MovieId = wonderwoman.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = batmanBegins.Id, GenreId = action.Id},
                    new MoviesGenres(){MovieId = theBatman.Id, GenreId = action.Id},
                    new MoviesGenres(){MovieId = airForceOne.Id, GenreId = action.Id},
                    new MoviesGenres(){MovieId = airForceOne.Id, GenreId = thriller.Id},
                    new MoviesGenres(){MovieId = thorLoveAndThunder.Id, GenreId = action.Id},
                    new MoviesGenres(){MovieId = thorLoveAndThunder.Id, GenreId = adventure.Id}
                });

            modelBuilder.Entity<MoviesActors>().HasData(
                new List<MoviesActors>()
                {
                    new MoviesActors(){MovieId = endgame.Id, ActorId = robertDowney.Id, Character = "Tony Stark", Order = 1},
                    new MoviesActors(){MovieId = endgame.Id, ActorId = chrisEvans.Id, Character = "Steve Rogers", Order = 2},
                    new MoviesActors(){MovieId = endgame.Id, ActorId = chrisHemsworth.Id, Character = "Thor", Order = 3},
                    new MoviesActors(){MovieId = endgame.Id, ActorId = markRuffalo.Id, Character = "Bruce Banner", Order = 4},
                    new MoviesActors(){MovieId = endgame.Id, ActorId = scarlettJohansson.Id, Character = "Scarlett Johansson", Order = 5},
                    new MoviesActors(){MovieId = iw.Id, ActorId = robertDowney.Id, Character = "Tony Stark", Order = 1},
                    new MoviesActors(){MovieId = iw.Id, ActorId = chrisEvans.Id, Character = "Steve Rogers", Order = 2},
                    new MoviesActors(){MovieId = iw.Id, ActorId = chrisHemsworth.Id, Character = "Thor", Order = 3},
                    new MoviesActors(){MovieId = iw.Id, ActorId = markRuffalo.Id, Character = "Bruce Banner", Order = 4},
                    new MoviesActors(){MovieId = iw.Id, ActorId = scarlettJohansson.Id, Character = "Scarlett Johansson", Order = 5},
                    new MoviesActors(){MovieId = sonic.Id, ActorId = jimCarrey.Id, Character = "Dr. Ivo Robotnik", Order = 1},
                    new MoviesActors(){MovieId = batmanBegins.Id, ActorId = christianBale.Id, Character = "Bruce Wayne/Batman", Order = 1},
                    new MoviesActors(){MovieId = theBatman.Id, ActorId = robertPattinson.Id, Character = "Bruce Wayne/Batman", Order = 1},
                    new MoviesActors(){MovieId = airForceOne.Id, ActorId = harrisonFord.Id, Character = "Indiana Jones", Order = 1},
                    new MoviesActors(){MovieId = thorLoveAndThunder.Id, ActorId = chrisHemsworth.Id, Character = "Thor", Order = 1},
                    new MoviesActors(){MovieId = wonderwoman.Id, ActorId = galGadot.Id, Character = "Wonder woman", Order = 1},
                    new MoviesActors(){MovieId = emma.Id, ActorId = anyaTaylorJoy.Id, Character = "Emma Woodhouse", Order = 1}
                });
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using WebAPIMovies.Entities;

namespace WebAPIMovies
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenders> MoviesGenders { get; set; }
        public DbSet<MoviesCinemas> MoviesCinemas { get; set; }

        private void SeedData(ModelBuilder modelBuilder)
        {

            /*var rolAdminId = "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d";
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
            };*/

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

            /*var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            modelBuilder.Entity<SalaDeCine>()
               .HasData(new List<SalaDeCine>
               {
                    //new SalaDeCine{Id = 1, Name = "Agora", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.9388777, 18.4839233))},
                    new SalaDeCine{Id = 4, Name = "Sambil", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.9118804, 18.4826214))},
                    new SalaDeCine{Id = 5, Name = "Megacentro", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.856427, 18.506934))},
                    new SalaDeCine{Id = 6, Name = "Village East Cinema", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-73.986227, 40.730898))}
               });*/

            var adventure = new Gender() { Id = 1, Name = "Adventure" };
            var animation = new Gender() { Id = 2, Name = "Animation" };
            var thriller = new Gender() { Id = 3, Name = "Thriller" };
            var romance = new Gender() { Id = 4, Name = "Romance" };
            var action = new Gender() { Id = 5, Name = "Action" };

            modelBuilder.Entity<Gender>()
                .HasData(new List<Gender>
                {
                    adventure, animation, thriller, romance, action
                });

            var jimCarrey = new Actor() { Id = 1, Name = "Jim Carrey", DateOfBirth = new DateTime(1962, 01, 17) };
            var robertDowney = new Actor() { Id = 2, Name = "Robert Downey Jr.", DateOfBirth = new DateTime(1965, 4, 4) };
            var chrisEvans = new Actor() { Id = 3, Name = "Chris Evans", DateOfBirth = new DateTime(1981, 06, 13) };
            var christianBale = new Actor() { Id = 4, Name = "Christian Bale", DateOfBirth = new DateTime(1974, 01, 30) };
            var robertPattinson = new Actor() { Id = 5, Name = "Robert Pattinson", DateOfBirth = new DateTime(1986, 05, 13) };

            modelBuilder.Entity<Actor>()
                .HasData(new List<Actor>
                {
                    jimCarrey, robertDowney, chrisEvans, christianBale, robertPattinson
                });

            var batmanBegins = new Movie()
            {
                Id = 1,
                Title = "Batman Begins",
                InTheaters = false,
                ReleaseDate = new DateTime(2005, 06, 16)
            };

            var endgame = new Movie()
            {
                Id = 2,
                Title = "Avengers: Endgame",
                InTheaters = true,
                ReleaseDate = new DateTime(2019, 04, 26)
            };

            var iw = new Movie()
            {
                Id = 3,
                Title = "Avengers: Infinity Wars",
                InTheaters = false,
                ReleaseDate = new DateTime(2019, 04, 26)
            };

            var sonic = new Movie()
            {
                Id = 4,
                Title = "Sonic the Hedgehog",
                InTheaters = false,
                ReleaseDate = new DateTime(2020, 02, 28)
            };
            var emma = new Movie()
            {
                Id = 5,
                Title = "Emma",
                InTheaters = false,
                ReleaseDate = new DateTime(2020, 02, 21)
            };
            var wonderwoman = new Movie()
            {
                Id = 6,
                Title = "Wonder Woman 1984",
                InTheaters = false,
                ReleaseDate = new DateTime(2020, 08, 14)
            };
            var theBatman = new Movie()
            {
                Id = 7,
                Title = "The Batman",
                InTheaters = true,
                ReleaseDate = new DateTime(2022, 03, 04)
            };

            modelBuilder.Entity<Movie>()
                .HasData(new List<Movie>
                {
                    batmanBegins, endgame, iw, sonic, emma,
                    wonderwoman, theBatman
                });

            modelBuilder.Entity<MoviesGenders>().HasData(
                new List<MoviesGenders>()
                {
                    new MoviesGenders(){MovieId = batmanBegins.Id, GenderId = action.Id},
                    new MoviesGenders(){MovieId = batmanBegins.Id, GenderId = thriller.Id},
                    new MoviesGenders(){MovieId = endgame.Id, GenderId = thriller.Id},
                    new MoviesGenders(){MovieId = endgame.Id, GenderId = adventure.Id},
                    new MoviesGenders(){MovieId = iw.Id, GenderId = thriller.Id},
                    new MoviesGenders(){MovieId = iw.Id, GenderId = adventure.Id},
                    new MoviesGenders(){MovieId = sonic.Id, GenderId = adventure.Id},
                    new MoviesGenders(){MovieId = emma.Id, GenderId = thriller.Id},
                    new MoviesGenders(){MovieId = emma.Id, GenderId = romance.Id},
                    new MoviesGenders(){MovieId = wonderwoman.Id, GenderId = thriller.Id},
                    new MoviesGenders(){MovieId = wonderwoman.Id, GenderId = adventure.Id},
                    new MoviesGenders(){MovieId = theBatman.Id, GenderId = action.Id},
                    new MoviesGenders(){MovieId = theBatman.Id, GenderId = thriller.Id},
                });

            modelBuilder.Entity<MoviesActors>().HasData(
                new List<MoviesActors>()
                {
                    new MoviesActors(){MovieId = batmanBegins.Id, ActorId = christianBale.Id, Character = "Bruce Wayne/Batman", Order = 1},
                    new MoviesActors(){MovieId = endgame.Id, ActorId = robertDowney.Id, Character = "Tony Stark", Order = 1},
                    new MoviesActors(){MovieId = endgame.Id, ActorId = chrisEvans.Id, Character = "Steve Rogers", Order = 2},
                    new MoviesActors(){MovieId = iw.Id, ActorId = robertDowney.Id, Character = "Tony Stark", Order = 1},
                    new MoviesActors(){MovieId = iw.Id, ActorId = chrisEvans.Id, Character = "Steve Rogers", Order = 2},
                    new MoviesActors(){MovieId = sonic.Id, ActorId = jimCarrey.Id, Character = "Dr. Ivo Robotnik", Order = 1},
                    new MoviesActors(){MovieId = theBatman.Id, ActorId = robertPattinson.Id, Character = "Bruce Wayne/Batman", Order = 1}
                });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MoviesGenders>()
                .HasKey(mg => new { mg.MovieId, mg.GenderId });

            modelBuilder.Entity<MoviesCinemas>()
                .HasKey(mc => new { mc.MovieId, mc.CinemaId });

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}

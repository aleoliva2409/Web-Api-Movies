using Microsoft.EntityFrameworkCore;
using WebAPIMovies.Entities;

namespace WebAPIMovies
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MoviesGenres>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });
            
            modelBuilder.Entity<MoviesCinemas>()
                .HasKey(mc => new { mc.MovieId, mc.CinemaId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }
        public DbSet<MoviesCinemas> MoviesCinemas { get; set; }
    }
}

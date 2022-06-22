using WebAPIMovies.Entities;

namespace WebAPIMovies.Tests.UnitTests
{
    [TestClass]
    public class MoviesControllerTests : BaseTests
    {
        private string CreateData()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var genre = new Genre() { Name = "genre 1" };

            var movies = new List<Movie>()
            {
                new Movie(){Title = "Movie 1", ReleaseDate = new DateTime(2010, 1,1), InTheaters = false},
                new Movie(){Title = "No Released", ReleaseDate = DateTime.Today.AddDays(1), InTheaters = false},
                new Movie(){Title = "Movie in theaters", ReleaseDate = DateTime.Today.AddDays(-1), InTheaters = true}
            };

            var movieWithGenre = new Movie()
            {
                Title = "Movie with Genre",
                ReleaseDate = new DateTime(2010, 1, 1),
                InTheaters = false
            };
            movies.Add(movieWithGenre);

            context.Add(genre);
            context.AddRange(movies);
            context.SaveChanges();

            var movieGenre = new MoviesGenres() { GenreId = genre.Id, MovieId = movieWithGenre.Id };
            context.Add(movieGenre);
            context.SaveChanges();

            return nameDB;
        }


    }
}

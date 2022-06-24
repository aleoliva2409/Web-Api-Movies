using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAPIMovies.Controllers;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Tests.UnitTests
{
    [TestClass]
    public class MoviesControllerTests : BaseTests
    {
        private async Task<string> CreateData()
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
            await context.SaveChangesAsync();

            var movieGenre = new MoviesGenres() { GenreId = genre.Id, MovieId = movieWithGenre.Id };
            context.Add(movieGenre);
            await context.SaveChangesAsync();

            return nameDB;
        }

        [TestMethod]
        public async Task FilterByTitle()
        {
            var nameDB = await CreateData();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var movieTitle = "Movie 1";

            var filterDTO = new MovieFilterDTO()
            {
                Title = movieTitle,
                QuantityPerPage = 10,
            };

            var response = await controller.GetFilter(filterDTO);
            var movies = response.Value;

            Assert.AreEqual(1, movies?.Count);
            Assert.AreEqual(movieTitle, movies?[0].Title);
        }

        [TestMethod]
        public async Task FilterByInTheaters()
        {
            var nameDB = await CreateData();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var filterDTO = new MovieFilterDTO()
            {
                InTheaters = true,
            };

            var response = await controller.GetFilter(filterDTO);
            var movies = response.Value;

            Assert.AreEqual(1, movies?.Count);
            Assert.AreEqual("Movie in theaters", movies?[0].Title);
        }

        [TestMethod]
        public async Task FilterByNextReleases()
        {
            var nameDB = await CreateData();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var filterDTO = new MovieFilterDTO()
            {
                NextReleases = true,
            };

            var response = await controller.GetFilter(filterDTO);
            var movies = response.Value;

            Assert.AreEqual(1, movies?.Count);
            Assert.AreEqual("No Released", movies?[0].Title);
        }

        [TestMethod]
        public async Task FilterByGenre()
        {
            var nameDB = await CreateData();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var genreId = context.Genres.Select(g =>  g.Id).First();

            var filterDTO = new MovieFilterDTO()
            {
                GenreId = genreId,
            };

            var response = await controller.GetFilter(filterDTO);
            var movies = response.Value;

            Assert.AreEqual(1, movies?.Count);
            Assert.AreEqual("Movie with Genre", movies?[0].Title);
        }

        [TestMethod]
        public async Task FilterWithOrderAsc()
        {
            var nameDB = await CreateData();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var filterDTO = new MovieFilterDTO()
            {
                Order = "title",
                OrderAsc = true
            };

            var response = await controller.GetFilter(filterDTO);
            var movies = response.Value;

            var newContext = BuildContext(nameDB);
            var moviesDB = await newContext.Movies.OrderBy(m => m.Title).ToListAsync();

            Assert.AreEqual(movies?.Count, moviesDB.Count);

            for (int i = 0; i < moviesDB.Count; i++)
            {
                var movieDB = moviesDB[i];
                var movieController = movies?[i];
                Assert.AreEqual(movieDB.Id, movieController?.Id);
            }

        }

        [TestMethod]
        public async Task FilterWithOrderDesc()
        {
            var nameDB = await CreateData();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var filterDTO = new MovieFilterDTO()
            {
                Order = "title",
                OrderAsc = false
            };

            var response = await controller.GetFilter(filterDTO);
            var movies = response.Value;

            var newContext = BuildContext(nameDB);
            var moviesDB = await newContext.Movies.OrderByDescending(m => m.Title).ToListAsync();

            Assert.AreEqual(movies?.Count, moviesDB.Count);

            for (int i = 0; i < moviesDB.Count; i++)
            {
                var movieDB = moviesDB[i];
                var movieController = movies?[i];
                Assert.AreEqual(movieDB.Id, movieController?.Id);
            }
        }

        [TestMethod]
        public async Task FilterWithOrderError()
        {
            var nameDB = await CreateData();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            var controller = new MoviesController(context, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var filterDTO = new MovieFilterDTO()
            {
                Order = "dateOfBirth",
                OrderAsc = true
            };

            var response = await controller.GetFilter(filterDTO);
            var movies = response.Value;

            var newContext = BuildContext(nameDB);
            var moviesDB = await newContext.Movies.ToListAsync();

            Assert.AreEqual(movies?.Count, moviesDB.Count);
        }
    }
}

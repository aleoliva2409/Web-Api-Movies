using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebAPIMovies.Controllers;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Tests.UnitTests
{
    [TestClass]
    public class ReviewsControllerTests : BaseTests
    {
        [TestMethod]
        public async Task UserDoesntCommentTwoReviews()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var context = BuildContext(nombreBD);
            await CreateMovies(nombreBD);

            var movieId = await context.Movies.Select(x => x.Id).FirstAsync();
            var review1 = new Review()
            {
                MovieId = movieId,
                UserId = userDefaultId,
                Score = 5
            };

            context.Add(review1);
            await context.SaveChangesAsync();

            var newContext = BuildContext(nombreBD);
            var mapper = ConfigAutoMapper();

            var controller = new ReviewsController(newContext, mapper);
            controller.ControllerContext = BuildControllerContext();

            var reviewCreateDTO = new ReviewCreateDTO { Score = 5 };
            var response = await controller.Post(movieId, reviewCreateDTO);

            var value = response as IStatusCodeActionResult;
            Assert.AreEqual(400, value?.StatusCode.Value);
        }

        [TestMethod]
        public async Task CreateReview()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var context = BuildContext(nombreBD);
            var mapper = ConfigAutoMapper();
            await CreateMovies(nombreBD);

            var movieId = context.Movies.Select(x => x.Id).First();
            var newContext1 = BuildContext(nombreBD);

            var controller = new ReviewsController(newContext1, mapper);
            controller.ControllerContext = BuildControllerContext();

            var reviewCreateDTO = new ReviewCreateDTO() { Score = 5 };
            var response = await controller.Post(movieId, reviewCreateDTO);

            var value = response as NoContentResult;
            Assert.IsNotNull(value);

            var newContext2 = BuildContext(nombreBD);
            var reviewDB = newContext2.Reviews.First();
            Assert.AreEqual(userDefaultId, reviewDB.UserId);
        }

        private async Task CreateMovies(string nombreDB)
        {
            var context = BuildContext(nombreDB);

            context.Movies.Add(new Movie() { Title = "Movie 1" });

            await context.SaveChangesAsync();
        }
    }
}

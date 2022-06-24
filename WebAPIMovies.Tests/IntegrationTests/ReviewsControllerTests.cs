using Newtonsoft.Json;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Tests.IntegrationTests
{
    [TestClass]
    public class ReviewsControllerTests : BaseTests
    {
        private static readonly string url = "/api/movies/1/reviews";

        [TestMethod]
        public async Task GetReviews_Return404_NoMovie()
        {
            var nameDB = Guid.NewGuid().ToString();
            var factory = BuildWebApplicationFactory(nameDB);

            var client = factory.CreateClient();
            var response = await client.GetAsync(url);
            Assert.AreEqual(404, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task GetReviews_EmptyList()
        {
            var nameDB = Guid.NewGuid().ToString();
            var factory = BuildWebApplicationFactory(nameDB);
            var context = BuildContext(nameDB);
            context.Movies.Add(new Movie() { Title = "Movie 1" });
            await context.SaveChangesAsync();

            var client = factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var reviews = JsonConvert.DeserializeObject<List<ReviewDTO>>
                (await response.Content.ReadAsStringAsync());
            Assert.AreEqual(0, reviews?.Count);
        }
    }
}

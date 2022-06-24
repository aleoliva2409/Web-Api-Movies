using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Tests.IntegrationTests
{
    [TestClass]
    public class GenresControllerTests : BaseTests
    {
        private static readonly string url = "/api/genres";

        [TestMethod]
        public async Task GetAllGenres_EmptyList()
        {
            var nameDB = Guid.NewGuid().ToString();
            var factory = BuildWebApplicationFactory(nameDB);

            var client = factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var genres = JsonConvert
                .DeserializeObject<List<GenreDTO>>(await response.Content.ReadAsStringAsync());

            Assert.AreEqual(0, genres?.Count);
        }

        [TestMethod]
        public async Task GetAllGenres()
        {
            var nameDB = Guid.NewGuid().ToString();
            var factory = BuildWebApplicationFactory(nameDB);

            var context = BuildContext(nameDB);
            context.Genres.Add(new Genre() { Name = "Genre 1" });
            context.Genres.Add(new Genre() { Name = "Genre 2" });
            await context.SaveChangesAsync();

            var client = factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var genres = JsonConvert
                .DeserializeObject<List<GenreDTO>>(await response.Content.ReadAsStringAsync());

            Assert.AreEqual(2, genres?.Count);
        }

        [TestMethod]
        public async Task DeleteGenre()
        {
            var nameDB = Guid.NewGuid().ToString();
            var factory = BuildWebApplicationFactory(nameDB);

            var context = BuildContext(nameDB);
            context.Genres.Add(new Genre() { Name = "Genre 1" });
            await context.SaveChangesAsync();

            var client = factory.CreateClient();
            var response = await client.DeleteAsync($"{url}/1");
            response.EnsureSuccessStatusCode();

            var newContext = BuildContext(nameDB);
            var exist = await newContext.Genres.AnyAsync();
            Assert.IsFalse(exist);
        }

        [TestMethod]
        public async Task DeleteGenre_Return401()
        {
            var nameDB = Guid.NewGuid().ToString();
            var factory = BuildWebApplicationFactory(nameDB, false);

            var client = factory.CreateClient();
            var response = await client.DeleteAsync($"{url}/1");
            Assert.AreEqual("Unauthorized", response.ReasonPhrase);
        }
    }
}

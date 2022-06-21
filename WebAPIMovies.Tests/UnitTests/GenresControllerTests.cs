using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIMovies.Controllers;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Tests.UnitTests
{
    [TestClass]
    public class GenresControllerTests : BaseTests
    {
        [TestMethod]
        public async Task GetAllGenres()
        {
            //Preparation
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            context.Genres.Add(new Genre() { Name = "Genre 1" });
            context.Genres.Add(new Genre() { Name = "Genre 2" });
            await context.SaveChangesAsync();

            var newContext = BuildContext(nameDB);

            //Prueba
            var controller = new GenresController(newContext, mapper);
            var response = await controller.Get();


            //Verificacion
            var genres = response.Value;
            Assert.AreEqual(2, genres.Count);
        }

        [TestMethod]
        public async Task GetGenreById_NoExist()
        {
            //Preparation
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            //Prueba
            var controller = new GenresController(context, mapper);
            var response = await controller.GetById(1);


            //Verificacion
            var result = response.Result as StatusCodeResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task GetGenreById_Exist()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            context.Genres.Add(new Genre() { Name = "Genre 1" });
            context.Genres.Add(new Genre() { Name = "Genre 2" });
            await context.SaveChangesAsync();

            var newContext = BuildContext(nameDB);

            var controller = new GenresController(newContext, mapper);

            var id = 2;
            var response = await controller.GetById(id);
            var result = response.Value;
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task CreateGenre()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();


            var controller = new GenresController(context, mapper);

            var newGenre = new GenreCreateDTO() { Name = "new genre" };
            var response = await controller.Post(newGenre);
            var result = response as CreatedAtRouteResult;
            Assert.IsNotNull(result);

            var newContext = BuildContext(nameDB);

            var genreName = await newContext.Genres.FirstOrDefaultAsync(g => g.Name == "new genre");
            var quantity = await newContext.Genres.CountAsync();
            Assert.AreEqual(genreName?.Name, "new genre");
            Assert.AreEqual(quantity, 1);
        }

        [TestMethod]
        public async Task UpdateGenre()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            context.Genres.Add(new Genre() { Name = "Genre 1" });
            await context.SaveChangesAsync();

            var newContext1 = BuildContext(nameDB);

            var controller = new GenresController(newContext1, mapper);
            var updateGenre = new GenreCreateDTO() { Name = "genre updated" };
            var genreId = 1;
            var response = await controller.Put(genreId, updateGenre);
            var result = response as StatusCodeResult;
            Assert.AreEqual(204, result.StatusCode);

            var newContext2 = BuildContext(nameDB);

            var exist = await newContext2.Genres.AnyAsync(g => g.Name == "genre updated");
            Assert.IsTrue(exist);
        }

        [TestMethod]
        public async Task DeleteGenre_NoExist()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            var controller = new GenresController(context, mapper);
            var genreId = 1;
            var response = await controller.Delete(genreId);
            var result = response as StatusCodeResult;
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteGenre()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            context.Genres.Add(new Genre() { Name = "Genre 1" });
            await context.SaveChangesAsync();

            var newContext1 = BuildContext(nameDB);

            var controller = new GenresController(newContext1, mapper);
            var genreId = 1;
            var response = await controller.Delete(genreId);
            var result = response as StatusCodeResult;
            Assert.AreEqual(204, result.StatusCode);

            var newContext2 = BuildContext(nameDB);

            var exist = await newContext2.Genres.AnyAsync(g => g.Id == genreId);
            Assert.IsFalse(exist);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Text;
using WebAPIMovies.Controllers;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;
using WebAPIMovies.Services;

namespace WebAPIMovies.Tests.UnitTests
{
    [TestClass]
    public class ActorsControllerTests : BaseTests
    {
        [TestMethod]
        public async Task GetActorsPaginate()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            context.Actors.Add(new Actor() { Name = "Actor 1" });
            context.Actors.Add(new Actor() { Name = "Actor 2" });
            context.Actors.Add(new Actor() { Name = "Actor 3" });
            await context.SaveChangesAsync();

            var newContext = BuildContext(nameDB);

            var controller = new ActorsController(newContext, mapper, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var page1 = await controller.Get(new PaginationDTO() { Page = 1, QuantityPerPage = 2 });
            var actorsPage1 = page1.Value;

            Assert.AreEqual(2, actorsPage1?.Count);

            // aca reiniciamos el HttpContext
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var page2 = await controller.Get(new PaginationDTO() { Page = 2, QuantityPerPage = 2 });
            var actorsPage2 = page2.Value;

            Assert.AreEqual(1, actorsPage2?.Count);

            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var page3 = await controller.Get(new PaginationDTO() { Page = 3, QuantityPerPage = 2 });
            var actorsPage3 = page3.Value;

            Assert.AreEqual(0, actorsPage3?.Count);
        }

        [TestMethod]
        public async Task CreateActorWithoutPhoto()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            var actor = new ActorCreateDTO() { Name = "Alejandro Oliva", DateOfBirth = DateTime.Now };

            var mock = new Mock<IStoreFiles>();
            mock.Setup(m => m.SaveFile(null, null, null, null))
                .Returns(Task.FromResult("url"));

            

            var controller = new ActorsController(context, mapper, mock.Object);

            var response = await controller.Post(actor);

            var result = response as CreatedAtRouteResult;
            Assert.AreEqual(201, result?.StatusCode);

            var newContext = BuildContext(nameDB);
            var list = await newContext.Actors.ToListAsync();

            Assert.AreEqual(1, list.Count);
            Assert.IsNull(list[0].Photo);

            Assert.AreEqual(0, mock.Invocations.Count);
        }

        [TestMethod]
        public async Task CreateActorWithPhoto()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();


            var content = Encoding.UTF8.GetBytes("Test image");
            var file = new FormFile(new MemoryStream(content), 0, content.Length,
                "Data", "image.jpg");

            file.Headers = new HeaderDictionary();
            file.ContentType = "image/jpg";

            var actor = new ActorCreateDTO()
            {
                Name = "Alejandro Oliva",
                DateOfBirth = DateTime.Now,
                Photo = file
            };

            var mock = new Mock<IStoreFiles>();
            mock.Setup(m => m.SaveFile(content, ".jpg", "actors", file.ContentType))
                .Returns(Task.FromResult("url"));



            var controller = new ActorsController(context, mapper, mock.Object);

            var response = await controller.Post(actor);

            var result = response as CreatedAtRouteResult;
            Assert.AreEqual(201, result?.StatusCode);

            var newContext = BuildContext(nameDB);
            var list = await newContext.Actors.ToListAsync();
            Console.WriteLine(list[0].Photo);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("url", list[0].Photo);
            Assert.AreEqual(1, mock.Invocations.Count);
        }

        [TestMethod]
        public async Task Patch_404ActorNoExist()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();


            var controller = new ActorsController(context, mapper, null);
            var patchDoc = new JsonPatchDocument<ActorUpdateDTO>();
            var response = await controller.Patch(1, patchDoc);
            var result = response as StatusCodeResult;
            Assert.AreEqual(404, result?.StatusCode);
        }

        [TestMethod]
        public async Task Patch_Exist()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigAutoMapper();

            var dateOfBirth = DateTime.Now;

            var actor = new Actor()
            {
                Name = "Alejandro Oliva",
                DateOfBirth = dateOfBirth,
            };

            context.Add(actor);
            await context.SaveChangesAsync();

            var newContext1 = BuildContext(nameDB);

            var controller = new ActorsController(newContext1, mapper, null);

            var mock = new Mock<IObjectModelValidator>();
            mock.Setup(m =>
            m.Validate(It.IsAny<ActionContext>(),
            It.IsAny<ValidationStateDictionary>(),
            It.IsAny<string>(), It.IsAny<object>()));

            controller.ObjectValidator = mock.Object;

            var patchDoc = new JsonPatchDocument<ActorUpdateDTO>();
            patchDoc.Operations.Add(new Operation<ActorUpdateDTO>(
                "replace", "/name", null, "Daniel Oliva"));
            var response = await controller.Patch(1, patchDoc);
            var result = response as StatusCodeResult;
            Assert.AreEqual(204, result?.StatusCode);

            var newContext2 = BuildContext(nameDB);
            var actorDB = await newContext2.Actors.FirstAsync();

            Assert.AreEqual("Daniel Oliva", actorDB.Name);
            Assert.AreEqual(dateOfBirth, actorDB.DateOfBirth);
        }
    }
}

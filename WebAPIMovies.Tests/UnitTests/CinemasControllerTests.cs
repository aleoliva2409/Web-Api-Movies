using NetTopologySuite;
using NetTopologySuite.Geometries;
using WebAPIMovies.Controllers;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Tests.UnitTests
{
    [TestClass]
    public class CinemasControllerTests : BaseTests
    {
        [TestMethod]
        public async Task GetCinemasNearby_5km()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            using (var context = LocalDbInitializer.GetDbContextLocalDb(false))
            {
                var cinema = new List<Cinema>()
                {
                    new Cinema{ Name = "Agora", Location = geometryFactory.CreatePoint(new Coordinate(-69.9388777, 18.4839233)) }
                };

                context.AddRange(cinema);
                await context.SaveChangesAsync();
            }

            var filter = new CinemaFilterDTO()
            {
                DistanceInKMs = 5,
                Latitude = 18.481139,
                Longitude = -69.938950
            };

            using (var context = LocalDbInitializer.GetDbContextLocalDb(false))
            {
                var mapper = ConfigAutoMapper();
                var controller = new CinemasController(context, mapper, geometryFactory);
                var response = await controller.Get(filter);
                var value = response.Value;
                Assert.AreEqual(1, value?.Count);
            }

        }
    }
}

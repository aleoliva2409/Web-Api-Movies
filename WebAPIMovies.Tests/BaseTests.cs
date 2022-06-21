using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using WebAPIMovies.Helpers;

namespace WebAPIMovies.Tests
{
    public class BaseTests
    {
        protected ApplicationDbContext BuildContext(string DBName)
        {
            var opt = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(DBName).Options;

            var dbContext = new ApplicationDbContext(opt);

            return dbContext;
        }

        protected IMapper ConfigAutoMapper()
        {
            var config = new MapperConfiguration(opt =>
            {
                var geometryFactory = NtsGeometryServices.Instance
                .CreateGeometryFactory(srid: 4326);
                opt.AddProfile(new AutoMapperProfiles(geometryFactory));
            });

            return config.CreateMapper();
        }
    }
}

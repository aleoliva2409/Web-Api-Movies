using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using System.Security.Claims;
using WebAPIMovies.Helpers;

namespace WebAPIMovies.Tests
{
    public class BaseTests
    {
        protected string userDefaultId = "9722b56a-77ea-4e41-941d-e319b6eb3712";
        protected string userDefaultEmail = "example@mail.com";

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

        protected ControllerContext BuildControllerContext()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userDefaultEmail),
                new Claim(ClaimTypes.Email, userDefaultEmail),
                new Claim(ClaimTypes.NameIdentifier, userDefaultId)
            }));

            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        protected WebApplicationFactory<Startup> BuildWebApplicationFactory(string nameBD,
            bool ignoreSecurity = true)
        {
            var factory = new WebApplicationFactory<Startup>();

            factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptorDBContext = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptorDBContext != null)
                    {
                        services.Remove(descriptorDBContext);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase(nameBD));

                    if (ignoreSecurity)
                    {
                        services.AddSingleton<IAuthorizationHandler, AllowAnonymousHandler>();

                        services.AddControllers(opt =>
                        {
                            opt.Filters.Add(new UserFalseFilter());
                        });
                    }
                });
            });

            return factory;
        }
    }
}

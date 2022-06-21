using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace WebAPIMovies.Helpers
{
    public class MovieExistAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly ApplicationDbContext dbContext;

        public MovieExistAttribute(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context,
            ResourceExecutionDelegate next)
        {
            var movieIdObject = context.HttpContext.Request.RouteValues["movieId"];

            if(movieIdObject == null)
            {
                return;
            }

            var movieId = int.Parse(movieIdObject.ToString());

            var existMovie = await dbContext.Movies.AnyAsync(m => m.Id == movieId);

            if (!existMovie)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                await next();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace WebAPIMovies.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParameters<T>(this HttpContext httpContext,
            IQueryable<T> queryable, int quantityPerPage)
        {
            double quantity = await queryable.CountAsync();
            double pages = Math.Ceiling(quantity / quantityPerPage);
            httpContext.Response.Headers.Add("totalPages", pages.ToString());
        }
    }
}

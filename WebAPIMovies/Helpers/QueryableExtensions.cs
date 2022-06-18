using WebAPIMovies.DTOs;

namespace WebAPIMovies.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ToPaginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable
                .Skip((paginationDTO.Page - 1) * paginationDTO.quantityPerPage)
                .Take(paginationDTO.quantityPerPage);
        }
    }
}

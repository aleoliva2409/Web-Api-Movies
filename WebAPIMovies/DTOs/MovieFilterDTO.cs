namespace WebAPIMovies.DTOs
{
    public class MovieFilterDTO
    {
        public int Page { get; set; } = 1;
        public int QuantityPerPage { get; set; } = 10;
        public PaginationDTO Pagination
        {
            get
            {
                return new PaginationDTO
                {
                    Page = Page,
                    QuantityPerPage = QuantityPerPage
                };
            }
        }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool InTheaters { get; set; }
        public bool NextReleases { get; set; }
    }
}

namespace WebAPIMovies.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        public int quantityPerPage = 10;
        private readonly int maxCountPerPage = 50;

        public int QuantityPerPage
        {
            get => quantityPerPage;
            set
            {
                quantityPerPage = value > maxCountPerPage ? maxCountPerPage : value;
            }
        }
    }
}

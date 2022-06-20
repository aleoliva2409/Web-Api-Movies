namespace WebAPIMovies.Entities
{
    public class MoviesCinemas
    {
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public Movie movie { get; set; }
        public Cinema cinema { get; set; }
    }
}

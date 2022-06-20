namespace WebAPIMovies.DTOs
{
    public class MovieIndexDTO
    {
        public List<MovieDTO> NextReleases { get; set; }
        public List<MovieDTO> InTheaters { get; set; }
    }
}

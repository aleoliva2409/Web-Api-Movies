using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs
{
    public class MovieUpdateDTO
    {
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs
{
    public class CinemaCreateDTO
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
    }
}

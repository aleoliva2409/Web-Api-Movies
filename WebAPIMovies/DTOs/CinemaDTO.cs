using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs
{
    public class CinemaDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }
    }
}

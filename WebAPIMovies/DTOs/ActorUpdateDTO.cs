using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs
{
    public class ActorUpdateDTO
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

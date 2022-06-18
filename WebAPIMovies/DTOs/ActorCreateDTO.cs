using System.ComponentModel.DataAnnotations;
using WebAPIMovies.Helpers;
using WebAPIMovies.Validations;

namespace WebAPIMovies.DTOs
{
    public class ActorCreateDTO
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        [FileWeightValidation(4)]
        [FileFormatValidation(ValidFileTypes.Image)]
        public IFormFile Photo { get; set; }
    }
}

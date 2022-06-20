using System.ComponentModel.DataAnnotations;
using WebAPIMovies.Helpers;

namespace WebAPIMovies.Entities
{
    public class Actor : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Photo { get; set; }
        public List<MoviesActors> MoviesActors { get; set; }
    }
}

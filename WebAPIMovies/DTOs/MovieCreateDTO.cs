using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPIMovies.Helpers;
using WebAPIMovies.Validations;

namespace WebAPIMovies.DTOs
{
    public class MovieCreateDTO
    {
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }

        [FileWeightValidation(4)]
        [FileFormatValidation(ValidFileTypes.Image)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenresIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorsMoviesCreateDTO>>))]
        public List<ActorsMoviesCreateDTO> Actors { get; set; }
    }
}

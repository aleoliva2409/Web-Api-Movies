using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Controllers
{
    [ApiController]
    [Route("api/genders")]
    public class GenresController : CustomBaseController
    {
        public GenresController(ApplicationDbContext context,
            IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            return await Get<Genre, GenreDTO>();
        }

        [HttpGet("{id:int}", Name = "getGenre")]
        public async Task<ActionResult<GenreDTO>> GetById(int id)
        {
            return await GetById<Genre, GenreDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreateDTO genderCreateDTO)
        {
            return await Post<GenreCreateDTO, Genre, GenreDTO>(genderCreateDTO, "getGenre");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreCreateDTO genderCreateDTO)
        {
            return await Put<GenreCreateDTO, Genre>(id, genderCreateDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Genre>(id);
        }
    }
}

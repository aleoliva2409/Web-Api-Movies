using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Controllers
{
    [ApiController]
    [Route("api/genders")]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            var gendersDB = await context.Genres.ToListAsync();
            var gendersMapped = mapper.Map<List<GenreDTO>>(gendersDB);
            return gendersMapped;
        }

        [HttpGet("{id:int}", Name = "getGender")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            var genderDB = await context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            
            if(genderDB == null)
            {
                return NotFound();
            }

            var genderMapped = mapper.Map<GenreDTO>(genderDB);
            return genderMapped;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreateDTO genderCreateDTO)
        {
            var gender = mapper.Map<Genre>(genderCreateDTO);
            context.Add(gender);
            await context.SaveChangesAsync();
            var genderDTO = mapper.Map<GenreDTO>(gender);

            return new CreatedAtRouteResult("getGender", new { id = genderDTO.Id }, genderDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreCreateDTO genderCreateDTO)
        {
            var gender = mapper.Map<Genre>(genderCreateDTO);
            gender.Id = id;
            context.Entry(gender).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isExistGender = await context.Genres.AnyAsync(g => g.Id == id);

            if (!isExistGender)
            {
                return NotFound();
            }

            context.Remove(new Genre() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}

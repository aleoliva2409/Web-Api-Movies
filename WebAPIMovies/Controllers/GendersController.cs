using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Controllers
{
    [ApiController]
    [Route("api/genders")]
    public class GendersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GendersController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenderDTO>>> Get()
        {
            var gendersDB = await context.Genders.ToListAsync();
            var gendersMapped = mapper.Map<List<GenderDTO>>(gendersDB);
            return gendersMapped;
        }

        [HttpGet("{id:int}", Name = "getGender")]
        public async Task<ActionResult<GenderDTO>> Get(int id)
        {
            var genderDB = await context.Genders.FirstOrDefaultAsync(g => g.Id == id);
            
            if(genderDB == null)
            {
                return NotFound();
            }

            var genderMapped = mapper.Map<GenderDTO>(genderDB);
            return genderMapped;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenderCreateDTO genderCreateDTO)
        {
            var gender = mapper.Map<Gender>(genderCreateDTO);
            context.Add(gender);
            await context.SaveChangesAsync();
            var genderDTO = mapper.Map<GenderDTO>(gender);

            return new CreatedAtRouteResult("getGender", new { id = genderDTO.Id }, genderDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenderCreateDTO genderCreateDTO)
        {
            var gender = mapper.Map<Gender>(genderCreateDTO);
            gender.Id = id;
            context.Entry(gender).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isExistGender = await context.Genders.AnyAsync(g => g.Id == id);

            if (!isExistGender)
            {
                return NotFound();
            }

            context.Remove(new Gender() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}

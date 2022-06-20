using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Controllers
{
    [ApiController]
    [Route("api/cinemas")]
    public class CinemasController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CinemasController(ApplicationDbContext context,
            IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CinemaDTO>>> Get()
        {
            return await Get<Cinema, CinemaDTO>();
        }

        [HttpGet("{id:int}", Name = "getCinema")]
        public async Task<ActionResult<CinemaDTO>> GetById(int id)
        {
            return await GetById<Cinema, CinemaDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CinemaCreateDTO cinemaCreateDTO)
        {
            return await Post<CinemaCreateDTO, Cinema, CinemaDTO>(cinemaCreateDTO, "getCinema");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaCreateDTO cinemaCreateDTO)
        {
            return await Put<CinemaCreateDTO, Cinema>(id, cinemaCreateDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Cinema>(id);
        }
    }
}

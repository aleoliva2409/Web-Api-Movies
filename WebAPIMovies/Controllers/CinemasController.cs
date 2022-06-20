using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
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
        private readonly GeometryFactory geometryFactory;

        public CinemasController(ApplicationDbContext context,
            IMapper mapper, GeometryFactory geometryFactory)
            : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometryFactory = geometryFactory;
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

        [HttpGet("filter")]
        public async Task<ActionResult<List<CinemaFilteredDTO>>> Get
            ([FromQuery] CinemaFilterDTO cinemaFilterDTO)
        {
            var userLocation = geometryFactory.CreatePoint(
                new Coordinate(cinemaFilterDTO.Longitude, cinemaFilterDTO.Latitude));

            var cinemas = await context.Cinemas
                .OrderBy(c => c.Location.Distance(userLocation))
                .Where(c => c.Location.IsWithinDistance(userLocation,
                cinemaFilterDTO.DistanceInKMs * 1000))
                .Select(c => new CinemaFilteredDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Latitude = c.Location.Y,
                    Longitude = c.Location.X,
                    DistanceInMeters = Math.Round(c.Location.Distance(userLocation))
                }).ToListAsync();

            return cinemas;
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

using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;
using WebAPIMovies.Helpers;
using WebAPIMovies.Services;

namespace WebAPIMovies.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IStoreFiles storeFiles;
        private readonly string container = "movies";

        public MoviesController(ApplicationDbContext context,
            IMapper mapper, IStoreFiles storeFiles)
        {
            this.context = context;
            this.mapper = mapper;
            this.storeFiles = storeFiles;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Movies.AsQueryable();
            await HttpContext.InsertPaginationParameters(queryable, paginationDTO.quantityPerPage);

            var moviesDB = await queryable.ToPaginate(paginationDTO).ToListAsync();
            var moviesMapped = mapper.Map<List<MovieDTO>>(moviesDB);
            return moviesMapped;
        }

        [HttpGet("{id:int}", Name = "getMovie")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movieDB = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movieDB == null)
            {
                return NotFound();
            }

            var movieMapped = mapper.Map<MovieDTO>(movieDB);
            return movieMapped;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] MovieCreateDTO movieCreateDTO)
        {
            var movie = mapper.Map<Movie>(movieCreateDTO);

            if (movieCreateDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await movieCreateDTO.Poster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(movieCreateDTO.Poster.FileName);
                    movie.Poster = await storeFiles.SaveFile(content, extension,
                        container, movieCreateDTO.Poster.ContentType);
                }
            }

            AsignOrderActors(movie);
            context.Add(movie);
            await context.SaveChangesAsync();
            var movieDTO = mapper.Map<MovieDTO>(movie);

            return new CreatedAtRouteResult("getMovie", new { id = movieDTO.Id }, movieDTO);
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] MovieCreateDTO movieCreateDTO)
        {
            var movieDB = await context.Movies
                .Include(m => m.MoviesActors)
                .Include(m => m.MoviesGenders)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movieDB == null)
            {
                return NotFound();
            }

            movieDB = mapper.Map(movieCreateDTO, movieDB);

            if (movieCreateDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await movieCreateDTO.Poster.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(movieCreateDTO.Poster.FileName);
                    movieDB.Poster = await storeFiles.EditFile(content, extension,
                        container, movieDB.Poster, movieCreateDTO.Poster.ContentType);
                }
            }
            
            AsignOrderActors(movieDB);
            await context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<MovieUpdateDTO> movieUpdateDTO)
        {
            if (movieUpdateDTO == null)
            {
                return BadRequest();
            }

            var movieDB = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movieDB == null)
            {
                return NotFound();
            }

            var movieDTO = mapper.Map<MovieUpdateDTO>(movieDB);
            movieUpdateDTO.ApplyTo(movieDTO, ModelState);

            var isValid = TryValidateModel(movieDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(movieDTO, movieDB);

            await context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isExistMovie = await context.Movies.AnyAsync(m => m.Id == id);

            if (!isExistMovie)
            {
                return NotFound();
            }

            context.Remove(new Movie() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }

        private void AsignOrderActors(Movie movie)
        {
            if (movie.MoviesActors != null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i + 1;
                }
            }
        }
    }
}

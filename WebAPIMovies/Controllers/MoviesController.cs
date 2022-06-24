using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;
using WebAPIMovies.Helpers;
using WebAPIMovies.Services;
using System.Linq.Dynamic.Core;

namespace WebAPIMovies.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IStoreFiles storeFiles;
        private readonly string container = "movies";

        public MoviesController(ApplicationDbContext context,
            IMapper mapper, IStoreFiles storeFiles) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.storeFiles = storeFiles;
        }

        [HttpGet]
        public async Task<ActionResult<MovieIndexDTO>> Get()
        {
            var top = 5;
            var today = DateTime.Today;

            var nextReleases = await context.Movies
                .Where(m => m.ReleaseDate > today)
                .OrderBy(m => m.ReleaseDate)
                .Take(top)
                .ToListAsync();

            var inTheaters = await context.Movies
                .Where(m => m.InTheaters)
                .Take(top)
                .ToListAsync();

            var result = new MovieIndexDTO()
            {
                InTheaters = mapper.Map<List<MovieDTO>>(inTheaters),
                NextReleases = mapper.Map<List<MovieDTO>>(nextReleases)
            };

            return result;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<MovieDTO>>> GetFilter([FromQuery] MovieFilterDTO movieFilterDTO)
        {
            var moviesQueryable = context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(movieFilterDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(m => m.Title.Contains(movieFilterDTO.Title));
            }

            if (movieFilterDTO.InTheaters)
            {
                moviesQueryable = moviesQueryable.Where(m => m.InTheaters);
            }

            if (movieFilterDTO.NextReleases)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(m => m.ReleaseDate > today);
            }

            if (movieFilterDTO.GenreId != 0)
            {
                moviesQueryable = moviesQueryable
                    .Where(m => m.MoviesGenres.Select(mg => mg.GenreId)
                    .Contains(movieFilterDTO.GenreId));
            }

            if (!string.IsNullOrEmpty(movieFilterDTO.Order))
            {
                var orderType = movieFilterDTO.OrderAsc ? "ascending" : "descending";

                try
                {
                    moviesQueryable = moviesQueryable.OrderBy($"{movieFilterDTO.Order} {orderType}");
                }
                catch
                {
                    // logger.LogError(ex.Message);
                    Console.WriteLine("Invalid filter");
                }
            }

            await HttpContext.InsertPaginationParameters(moviesQueryable, movieFilterDTO.QuantityPerPage);

            var movies = await moviesQueryable.ToPaginate(movieFilterDTO.Pagination).ToListAsync();

            return mapper.Map<List<MovieDTO>>(movies);
        }

        [HttpGet("{id:int}", Name = "getMovie")]
        public async Task<ActionResult<MovieDetailDTO>> GetById(int id)
        {
            var movieDB = await context.Movies
                .Include(m => m.MoviesActors).ThenInclude(ma => ma.Actor)
                .Include(m => m.MoviesGenres).ThenInclude(mg => mg.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movieDB == null)
            {
                return NotFound();
            }

            movieDB.MoviesActors = movieDB.MoviesActors.OrderBy(ma => ma.Order).ToList();
            return mapper.Map<MovieDetailDTO>(movieDB);
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
                .Include(m => m.MoviesGenres)
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
            return await Patch<Movie, MovieUpdateDTO>(id, movieUpdateDTO);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Movie>(id);
        }

        private static void AsignOrderActors(Movie movie)
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

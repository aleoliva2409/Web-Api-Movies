using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;
using WebAPIMovies.Helpers;

namespace WebAPIMovies.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId:int}/reviews")]
    [ServiceFilter(typeof(MovieExistAttribute))]
    public class ReviewsController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ReviewsController(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewDTO>>> Get(int movieId,
            [FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Reviews.Include(r => r.User).AsQueryable();
            queryable = queryable.Where(q => q.MovieId == movieId);
            return await Get<Review, ReviewDTO>(paginationDTO, queryable);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int movieId,
            [FromBody] ReviewCreateDTO reviewCreateDTO)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(u =>
            u.Type == ClaimTypes.NameIdentifier).Value;

            var existReview = await context.Reviews.AnyAsync(r => r.MovieId == movieId
            && r.UserId == userId);

            if(existReview)
            {
                return BadRequest("Review already exist");
            }

            var review = mapper.Map<Review>(reviewCreateDTO);
            review.MovieId = movieId;
            review.UserId = userId;

            context.Add(review);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int movieId, int reviewId,
            [FromBody] ReviewCreateDTO reviewCreateDTO)
        {
            var reviewDB = await context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);

            if (reviewDB == null)
            {
                return NotFound();
            }

            var userId = HttpContext.User.Claims.FirstOrDefault(u =>
            u.Type == ClaimTypes.NameIdentifier).Value;

            if (reviewDB.UserId != userId)
            {
                return Forbid();
            }

            reviewDB = mapper.Map(reviewCreateDTO, reviewDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int reviewId)
        {
            var reviewDB = await context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);

            if (reviewDB == null)
            {
                return NotFound();
            }

            var userId = HttpContext.User.Claims.FirstOrDefault(u =>
            u.Type == ClaimTypes.NameIdentifier).Value;

            if (reviewDB.UserId != userId)
            {
                return Forbid();
            }

            context.Remove(reviewDB);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}

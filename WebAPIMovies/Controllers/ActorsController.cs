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
    [Route("api/actors")]
    public class ActorsController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IStoreFiles storeFiles;
        private readonly string container = "actors";


        public ActorsController(ApplicationDbContext context, IMapper mapper,
            IStoreFiles storeFiles) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.storeFiles = storeFiles;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            return await Get<Actor, ActorDTO>(paginationDTO);
        }

        [HttpGet("{id:int}", Name = "getActor")]
        public async Task<ActionResult<ActorDTO>> GetById(int id)
        {
            return await GetById<Actor, ActorDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreateDTO actorCreateDTO)
        {
            var actor = mapper.Map<Actor>(actorCreateDTO);

            if (actorCreateDTO.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreateDTO.Photo.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreateDTO.Photo.FileName);
                    actor.Photo = await storeFiles.SaveFile(content, extension,
                        container, actorCreateDTO.Photo.ContentType);
                }
            }

            context.Add(actor);
            await context.SaveChangesAsync();
            var actorDTO = mapper.Map<ActorDTO>(actor);

            return new CreatedAtRouteResult("getActor", new { id = actorDTO.Id }, actorDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreateDTO actorCreateDTO)
        {
            var actorDB = await context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (actorDB == null)
            {
                return NotFound();
            }

            actorDB = mapper.Map(actorCreateDTO, actorDB);

            if (actorCreateDTO.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreateDTO.Photo.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreateDTO.Photo.FileName);
                    actorDB.Photo = await storeFiles.EditFile(content, extension,
                        container, actorDB.Photo, actorCreateDTO.Photo.ContentType);
                }
            }

            await context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorUpdateDTO> actorUpdateDTO)
        {
            return await Patch<Actor, ActorUpdateDTO>(id, actorUpdateDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Actor>(id);
        }
    }
}

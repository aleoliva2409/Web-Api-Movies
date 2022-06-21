using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIMovies.DTOs;
using WebAPIMovies.Helpers;

namespace WebAPIMovies.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        protected async Task<List<TDTO>> Get<TEntity, TDTO>() where TEntity : class
        {
            var entities = await context.Set<TEntity>().AsNoTracking().ToListAsync();
            var dtos = mapper.Map<List<TDTO>>(entities);

            return dtos;
        }

        protected async Task<List<TDTO>> Get<TEntity, TDTO>
            (PaginationDTO paginationDTO) where TEntity : class
        {
            var queryable = context.Set<TEntity>().AsQueryable();
            return await Get<TEntity, TDTO>(paginationDTO, queryable);
        }

        protected async Task<List<TDTO>> Get<TEntity, TDTO>
            (PaginationDTO paginationDTO, IQueryable<TEntity> queryable)
            where TEntity : class
        {
            await HttpContext.InsertPaginationParameters(queryable, paginationDTO.quantityPerPage);
            var entities = await queryable.ToPaginate(paginationDTO).ToListAsync();
            return mapper.Map<List<TDTO>>(entities);
        }

        protected async Task<ActionResult<TDTO>> GetById<TEntity, TDTO>(int id) where TEntity : class, IId
        {
            var entity = await context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if(entity == null)
            {
                return NotFound();
            }
            
            return mapper.Map<TDTO>(entity);
        }
        
        protected async Task<ActionResult> Post<TCreateDTO, TEntity, TDTO>
            (TCreateDTO createDTO, string route) where TEntity : class, IId
        {
            var entity = mapper.Map<TEntity>(createDTO);
            context.Add(entity);
            await context.SaveChangesAsync();
            var dto = mapper.Map<TDTO>(entity);

            return new CreatedAtRouteResult(route, new { id = entity.Id }, dto);
        }
        
        protected async Task<ActionResult> Put<TCreateDTO, TEntity>
            (int id, TCreateDTO createDTO) where TEntity : class, IId
        {
            var entity = mapper.Map<TEntity>(createDTO);
            entity.Id = id;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }
        
        protected async Task<ActionResult> Patch<TEntity, TUpdateDTO>
            (int id, JsonPatchDocument<TUpdateDTO> patchDocument)
            where TEntity : class, IId
            where TUpdateDTO : class
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            
            var entityDB = await context.Set<TEntity>().FirstOrDefaultAsync(a => a.Id == id);
            
            if (entityDB == null)
            {
                return NotFound();
            }

            var entityDTO = mapper.Map<TUpdateDTO>(entityDB);
            patchDocument.ApplyTo(entityDTO, ModelState);

            var isValid = TryValidateModel(entityDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(entityDTO, entityDB);

            await context.SaveChangesAsync();

            return NoContent();
        }

        // aca vemos como TEntity es un class y aparte de decimos que tiene que
        // cumplir con la interface IId y ademas tiene un consctructor vacio
        // indicado como "new()"
        protected async Task<ActionResult> Delete<TEntity>
            (int id) where TEntity : class, IId, new()
        {
            var isExistEntity = await context.Set<TEntity>().AnyAsync(x => x.Id == id);

            if (!isExistEntity)
            {
                return NotFound();
            }

            context.Remove(new TEntity() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}

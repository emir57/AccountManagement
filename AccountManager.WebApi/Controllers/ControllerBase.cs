using AccountManager.Business.Abstract;
using AutoMapper;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AccountManager.WebApi.Controllers
{
    public class BaseController<TDto, TEntity> : ControllerBase
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
    {
        protected IAsyncBaseService<TDto, TEntity> BaseService;
        protected readonly IMapper Mapper;
        public BaseController(IAsyncBaseService<TDto, TEntity> baseService, IMapper mapper)
        {
            BaseService = baseService;
            Mapper = mapper;
        }

        [NonAction]
        public async Task<IActionResult> AddAsync(TDto dto)
        {
            var result = await BaseService.AddAsync(dto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [NonAction]
        public async Task<IActionResult> UpdateAsync(int id, TDto dto)
        {
            var result = await BaseService.UpdateAsync(id, dto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [NonAction]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await BaseService.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [NonAction]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await BaseService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [NonAction]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await BaseService.GetAllAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}

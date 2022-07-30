using Core.Entity;
using Core.Utilities.Results;

namespace AccountManager.Business.Abstract
{
    public interface IAsyncBaseService<TDto, TEntity>
        where TDto : class, IDto, new()
        where TEntity : class, IEntity, new()
    {
        Task<IResult> AddAsync(TDto entity);
        Task<IResult> UpdateAsync(int id, TDto entity);
        Task<IResult> DeleteAsync(int id);

        Task<IDataResult<TDto>> GetByIdAsync(int id);
        Task<IDataResult<List<TDto>>> GetAllAsync();
    }
}

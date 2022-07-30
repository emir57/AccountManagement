using AccountManager.Business.Abstract;
using AutoMapper;
using Core.DataAccess;
using Core.Entity;
using Core.Utilities.Results;
using AccountManager.Business.Constants;

namespace AccountManager.Business.Concrete
{
    public class AsyncBaseManager<TDto, TEntity> : IAsyncBaseService<TDto, TEntity>
        where TDto : class, IDto, new()
        where TEntity : class, IEntity, new()
    {
        protected readonly IAsyncEntityRepository<TEntity> Repository;
        protected readonly IMapper Mapper;

        public AsyncBaseManager(IAsyncEntityRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<IResult> AddAsync(TDto entity)
        {
            TEntity addedEntity = Mapper.Map<TEntity>(entity);
            bool result = await Repository.AddAsync(addedEntity);
            if (result)
                return new SuccessResult(BusinessMessages.SuccessAdd);
            return new ErrorResult(BusinessMessages.UnSuccessAdd);
        }

        public virtual async Task<IResult> DeleteAsync(int id)
        {
            TEntity deletedEntity = await Repository.GetByIdAsync(id);
            if (deletedEntity == null)
                return new ErrorResult();
            bool result = await Repository.DeleteAsync(deletedEntity);
            if (result)
                return new SuccessResult(BusinessMessages.SuccessDelete);
            return new ErrorResult(BusinessMessages.UnSuccessDelete);
        }

        public virtual async Task<IDataResult<List<TDto>>> GetAllAsync()
        {
            var result = await Repository.GetAllAsync();
            var returnValue = Mapper.Map<IEnumerable<TDto>>(result);
            return new SuccessDataResult<List<TDto>>(returnValue.ToList(), BusinessMessages.SuccessList);
        }

        public virtual async Task<IDataResult<TDto>> GetByIdAsync(int id)
        {
            TEntity entity = await Repository.GetByIdAsync(id);
            if (entity != null)
                return new SuccessDataResult<TDto>(Mapper.Map<TDto>(entity), BusinessMessages.SuccessGet);
            return new ErrorDataResult<TDto>(BusinessMessages.UnSuccessGet);
        }

        public virtual async Task<IResult> UpdateAsync(int id, TDto entity)
        {
            TEntity updatedEntity = await Repository.GetByIdAsync(id);
            if (updatedEntity == null)
                return new ErrorResult(BusinessMessages.NotFound);
            Mapper.Map(updatedEntity, entity);
            bool result = await Repository.UpdateAsync(updatedEntity);
            if (result)
                return new SuccessResult(BusinessMessages.SuccessUpdate);
            return new ErrorResult(BusinessMessages.UnSuccessUpdate);
        }
    }
}

using AccountManager.Business.Abstract;
using AccountManager.Business.Constants;
using AccountManager.Business.ValidationRules.FluentValidation;
using AccountManager.Data.Abstract;
using AccountManager.Dto.Concrete;
using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entity.Concrete;
using Core.Utilities.Results;

namespace AccountManager.Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class AccountManager : AsyncBaseManager<AccountDto, Account>, IAccountService
    {
        public AccountManager(IAccountDal repository, IMapper mapper) : base(repository, mapper)
        {
        }
        [ValidationAspect(typeof(AccountValidator))]
        public override Task<IResult> AddAsync(AccountDto entity)
        {
            return base.AddAsync(entity);
        }

        public async Task<IDataResult<Account>> GetAccountByEmailOrUsername(string emailOrUsername)
        {
            var account = await Repository.GetAsync(a => a.UserName.ToLower() == emailOrUsername.ToLower() || a.Email.ToLower() == emailOrUsername.ToLower());
            if (account == null)
                return new ErrorDataResult<Account>(BusinessMessages.NotFound);
            return new SuccessDataResult<Account>(account);
        }
        [ValidationAspect(typeof(AccountValidator))]
        public override async Task<IResult> UpdateAsync(int id, AccountDto entity)
        {
            Account updatedEntity = await Repository.GetByIdAsync(id);
            if (updatedEntity == null)
                return new ErrorResult(BusinessMessages.NotFound);

            updatedEntity.Email = entity.Email;
            updatedEntity.Name = entity.Name;
            updatedEntity.UserName = entity.UserName;

            bool result = await Repository.UpdateAsync(updatedEntity);
            if (result)
                return new SuccessResult(BusinessMessages.SuccessUpdate);
            return new ErrorResult(BusinessMessages.UnSuccessUpdate);
        }
    }
}

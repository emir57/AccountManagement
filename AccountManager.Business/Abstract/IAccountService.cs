using AccountManager.Dto.Concrete;
using Core.Entity.Concrete;
using Core.Utilities.Results;

namespace AccountManager.Business.Abstract
{
    public interface IAccountService : IAsyncBaseService<AccountDto, Account>
    {
        Task<IDataResult<Account>> GetAccountByEmailOrUsername(string emailOrUsername);
    }
}

using AccountManager.Dto.Concrete;
using Core.Entity.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;

namespace AccountManager.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<Account>> LoginAsync(LoginDto loginDto);
        Task<IResult> RegisterAsync(RegisterDto registerDto);
        Task<IDataResult<AccessToken>> CreateAccessTokenAsync(Account account);
    }
}

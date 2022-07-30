using AccountManager.Business.Abstract;
using AccountManager.Business.Constants;
using AccountManager.Business.ValidationRules.FluentValidation;
using AccountManager.Dto.Concrete;
using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entity.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;

namespace AccountManager.Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class AuthManager : IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AuthManager(IAccountService accountService, ITokenHelper tokenHelper, IMapper mapper)
        {
            _accountService = accountService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessTokenAsync(Account account)
        {
            return await Task.Run(() =>
            {
                return new SuccessDataResult<AccessToken>(_tokenHelper.CreateToken(account),BusinessMessages.SuccessCreateToken);
            });
        }
        [ValidationAspect(typeof(LoginValidator))]
        public async Task<IDataResult<Account>> LoginAsync(LoginDto loginDto)
        {
            var accountCheck = await _accountService.GetAccountByEmailOrUsername(loginDto.UserNameOrEmail);
            if (accountCheck.Success == false)
                return new ErrorDataResult<Account>(BusinessMessages.NotFoundAccount);
            if (HashingHelper.VerifyPasswordHash(
                loginDto.Password,
                accountCheck.Data.PasswordHash,
                accountCheck.Data.PasswordSalt) == false)
            {
                return new ErrorDataResult<Account>(BusinessMessages.WrongPassword);
            }
            return new SuccessDataResult<Account>(accountCheck.Data);
        }
        [ValidationAspect(typeof(RegisterValidator))]
        public async Task<IResult> RegisterAsync(RegisterDto registerDto)
        {
            var accountCheck = await _accountService.GetAccountByEmailOrUsername(registerDto.Email);
            if (accountCheck.Success == true)
                return new ErrorResult(BusinessMessages.UserAlreadyExists);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);
            AccountDto accountDto = _mapper.Map<AccountDto>(registerDto);

            accountDto.PasswordHash = passwordHash;
            accountDto.PasswordSalt = passwordSalt;

            var addResult = await _accountService.AddAsync(accountDto);
            if (addResult.Success == false)
                return new ErrorResult(BusinessMessages.UnSuccessRegister);
            return new SuccessResult(BusinessMessages.SuccessRegister);
        }
    }
}

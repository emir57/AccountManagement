using AccountManager.Business.Abstract;
using AccountManager.Business.Helpers;
using AccountManager.Dto.Concrete;
using AutoMapper;
using Core.Entity.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Moq;

namespace AccountManager.Tests.Business
{
    public class AuthorizationTests
    {
        private Mock<IAccountService> _accountService;
        private Mock<ITokenHelper> _tokenHandler;
        private Mock<IAuthService> _authService;

        public AuthorizationTests()
        {
            var config = new MapperConfiguration(x => x.AddProfile(new AutoMapperHelper()));
            var mapper = config.CreateMapper();

            _accountService = new Mock<IAccountService>();
            _tokenHandler = new Mock<ITokenHelper>();
            _authService = new Mock<IAuthService>();
        }
        [Fact]
        public async Task Login()
        {
            _authService.Setup(x => x.LoginAsync(It.IsAny<LoginDto>())).ReturnsAsync(new SuccessDataResult<Account>(getAccount()));

            var result = await _authService.Object.LoginAsync(getUser());

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Register()
        {
            _authService.Setup(x => x.RegisterAsync(It.IsAny<RegisterDto>())).ReturnsAsync((RegisterDto dto) => new SuccessResult());

            var result = await _authService.Object.RegisterAsync(getRegisterDto());

            Assert.True(result.Success);
        }

        [Fact]
        public void AccessToken()
        {
            _tokenHandler.Setup(x => x.CreateToken(It.IsAny<Account>())).Returns((Account account) => new AccessToken
            {
                Token = "Test Token",
                Expiration = DateTime.Now.AddMinutes(5)
            });

            var result = _tokenHandler.Object.CreateToken(getAccount());

            Assert.NotNull(result.Token);
        }

        private LoginDto getUser()
        {
            return new LoginDto
            {
                UserNameOrEmail = "emir@hotmail.com",
                Password = "123"
            };
        }
        private Account getAccount()
        {
            return new Account
            {
                Id = 1,
                UserName = "emir",
                Email = "emir@hotmail.com",
                Name = "Emir Gürbüz",
                PasswordHash = new byte[2],
                PasswordSalt = new byte[2]
            };
        }
        private RegisterDto getRegisterDto()
        {
            return new RegisterDto
            {
                UserName = "emir",
                Email = "emir@hotmail.com",
                Name = "Emir Gürbüz",
                Password = "123",
            };
        }
    }
}

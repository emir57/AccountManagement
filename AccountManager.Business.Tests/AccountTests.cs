using AccountManager.Business.Abstract;
using AccountManager.Dto.Concrete;
using Core.Entity.Concrete;
using Core.Utilities.Results;
using Moq;

namespace AccountManager.Business.Tests
{
    public class AccountTests
    {
        Mock<IAccountService> _accountServiceMock;
        public AccountTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
        }
        [Fact]
        public async Task Get_list()
        {
            _accountServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new SuccessDataResult<List<AccountDto>>(getAccountDtos()));

            var result = await _accountServiceMock.Object.GetAllAsync();

            Assert.Equal(result.Data.Count, 2);
        }
        [Theory]
        [InlineData(1)]
        public async Task Get_by_id(int id)
        {
            _accountServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => new SuccessDataResult<AccountDto>(getAccountDtoById(id)));

            var result = await _accountServiceMock.Object.GetByIdAsync(id);

            Assert.NotNull(result.Data);
        }
        [Fact]
        public async Task Add_account()
        {
            _accountServiceMock.Setup(x => x.AddAsync(It.IsAny<AccountDto>())).ReturnsAsync((AccountDto dto) => new SuccessResult());

            var result = await _accountServiceMock.Object.AddAsync(new AccountDto
            {
                Name = "TestName",
                Email = "TestEmail",
                UserName = "TestUsername",
                PasswordHash = new byte[2],
                PasswordSalt = new byte[2]
            });

            Assert.True(result.Success);
        }
        [Theory]
        [InlineData(1)]
        public async Task Update_account(int id)
        {
            _accountServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<AccountDto>())).ReturnsAsync((int id, AccountDto dto) => new SuccessResult());

            var result = await _accountServiceMock.Object.UpdateAsync(id, new AccountDto
            {
                Name = "TestName2",
                Email = "TestEmail2",
                UserName = "TestUsername2"
            });

            Assert.True(result.Success);
        }
        [Theory]
        [InlineData(1)]
        public async Task Delete_account(int id)
        {
            _accountServiceMock.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync((int id) => new SuccessResult());

            var result = await _accountServiceMock.Object.DeleteAsync(id);

            Assert.True(result.Success);
        }
        private List<AccountDto> getAccountDtos()
        {
            return new List<AccountDto>
            {
                new AccountDto{Name="Emir",UserName="emir",Email="emir@hotmail.com"},
                new AccountDto{Name="Yasin",UserName="yasin",Email="yasin@hotmail.com"}
            };
        }
        private List<Account> getAccounts()
        {
            return new List<Account>
            {
                new Account{Id=1,Name="Emir",UserName="emir",Email="emir@hotmail.com",PasswordHash=new byte[1],PasswordSalt=new byte[1]},
                new Account{Id=2, Name="Yasin",UserName="yasin",Email="yasin@hotmail.com",PasswordHash=new byte[1],PasswordSalt=new byte[1]}
            };
        }
        private AccountDto getAccountDtoById(int id)
        {
            var account = getAccounts().SingleOrDefault(x => x.Id == id);
            return new AccountDto
            {
                Email = account.Email,
                Name = account.Name,
                UserName = account.UserName
            };
        }
    }
}

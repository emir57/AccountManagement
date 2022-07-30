using Core.Entity;

namespace AccountManager.Dto.Concrete
{
    public class AccountDto : IDto
    {
        public string UserName { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

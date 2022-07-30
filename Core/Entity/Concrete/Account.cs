using System.ComponentModel.DataAnnotations;

namespace Core.Entity.Concrete
{
    public class Account : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastActivity { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

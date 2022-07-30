using Core.Entity;

namespace AccountManager.Dto.Concrete
{
    public class PersonDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FullName
        {
            get
            {
                return string.Concat(FirstName, " ", LastName);
            }
        }
    }
}

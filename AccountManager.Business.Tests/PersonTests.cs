using AccountManager.Business.Abstract;
using AccountManager.Entity.Concrete;
using Core.Utilities.Results;
using Moq;

namespace AccountManager.Business.Tests
{
    public class PersonTests
    {
        Mock<IPersonService> _personServiceMock;
        public PersonTests()
        {
            _personServiceMock = new Mock<IPersonService>();
        }
        [Fact]
        public async Task Get_persons()
        {
            _personServiceMock.Setup(x => x.GetPersonsAsync()).ReturnsAsync(new SuccessDataResult<List<Person>>(getPersons()));

            var result = await _personServiceMock.Object.GetPersonsAsync();

            Assert.Equal(result.Data.Count, 2);
        }

        private List<Person> getPersons()
        {
            return new List<Person>
            {
                new Person{AccountId=1, FirstName="Emir",LastName="Gürbüz",Email="emir@hotmail.com",Phone="212"},
                new Person{AccountId=1, FirstName="Yasin",LastName="Uçan",Email="yasin@hotmail.com",Phone="213"},
            };
        }
    }
}

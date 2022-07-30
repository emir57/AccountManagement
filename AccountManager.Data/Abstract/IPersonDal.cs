using AccountManager.Entity.Concrete;
using Core.DataAccess;

namespace AccountManager.Data.Abstract
{
    public interface IPersonDal : IAsyncEntityRepository<Person>
    {
    }
}

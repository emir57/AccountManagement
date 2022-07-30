using Core.DataAccess;
using Core.Entity.Concrete;

namespace AccountManager.Data.Abstract
{
    public interface IAccountDal : IAsyncEntityRepository<Account>
    {
    }
}

using AccountManager.Data.Abstract;
using AccountManager.Data.Contexts;
using Core.DataAccess.EntityFramework;
using Core.Entity.Concrete;

namespace AccountManager.Data.Concrete.EntityFramework
{
    public class EfAccountDal : EfRepositoryBase<Account, AccountManagementDbContext>, IAccountDal
    {
        
    }
}

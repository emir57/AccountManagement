using AccountManager.Data.Contexts;
using AccountManager.Entity.Concrete;
using Core.DataAccess.EntityFramework;
using AccountManager.Data.Abstract;

namespace AccountManager.Data.Concrete.EntityFramework
{
    public class EfPersonDal : EfRepositoryBase<Person, AccountManagementDbContext>, IPersonDal
    {
    }
}

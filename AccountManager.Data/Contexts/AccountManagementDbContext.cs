using AccountManager.Entity.Concrete;
using Core.Entity;
using Core.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AccountManager.Data.Contexts
{
    public class AccountManagementDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .ToTable("Accounts");

            modelBuilder.Entity<Person>()
                .HasNoKey()
                .ToTable("Persons");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<IEntity>();
            foreach (var data in datas)
            {
                var _ = data.State switch
                {
                    EntityState.Deleted => data.Entity.DeletedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(getConnectionString("SqlServerConnection"));
        }

        private string getConnectionString(string conName)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config.GetConnectionString(conName);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SpyStore.Models.Entities;

namespace SpyStore.DAL.EF
{
    public class StoreContext : DbContext
    {
        #region Properties

        public DbSet<Category> Categories { get; set; }

        #endregion Properties

        #region Constructors

        public StoreContext() { }

        public StoreContext(DbContextOptions options) : base(options) { }

        #endregion Constructors

        #region Overriden Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpyStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                optionsBuilder.UseSqlServer(connectionString, options => options.ExecutionStrategy(d => new MyExecutionStrategy(d)));
            }
        }

        #endregion Overriden Methods
    }
}

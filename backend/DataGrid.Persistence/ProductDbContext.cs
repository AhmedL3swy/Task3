using DataGrid.Domain;
using DataGrid.Persistence.Configurations;
using DataGrid.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace DataGrid.Persistence
{
    class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region DbSets
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User configuration
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            UserSeedData.Seed(modelBuilder);
            #endregion

        }
    }
}

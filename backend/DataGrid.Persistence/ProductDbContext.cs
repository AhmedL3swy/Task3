using Microsoft.EntityFrameworkCore;

namespace DataGrid.Persistence
{
    class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        // DBsets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}

using Microsoft.EntityFrameworkCore;
using ProductService.Entity;

namespace ProductService
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductDbModel> Product { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

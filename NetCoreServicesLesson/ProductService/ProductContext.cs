using Microsoft.EntityFrameworkCore;
using ProductService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductDbModel> Product { get; set; }

        public ProductContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

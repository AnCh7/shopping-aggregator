using Microsoft.EntityFrameworkCore;
using ShopAggregator.Api.Models;

namespace ShopAggregator.Api.Db
{
    public class ShopAggregatorContext : DbContext
    {
        public ShopAggregatorContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<ShopProduct> Stock { get; set; }
    }
}
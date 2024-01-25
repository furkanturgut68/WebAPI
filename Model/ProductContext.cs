using Microsoft.EntityFrameworkCore;

namespace WebAPI.Model
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 1, ProductName = "Deneme 1", Price = 7500, IsActive = false });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 2, ProductName = "Deneme 2", Price = 8500, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 3, ProductName = "Deneme 3", Price = 8500, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 4, ProductName = "Deneme 4", Price = 10500, IsActive = false });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 5, ProductName = "Deneme 5", Price = 11500, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 6, ProductName = "Deneme 6", Price = 12500, IsActive = true });

        }
        public DbSet<Product> Products { get; set; }

    }
}

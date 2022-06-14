using Microsoft.EntityFrameworkCore;

namespace WebShop.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<Product> Products { get; set;}

        public DbSet<Order> Orders { get; set;}

        public DbSet<User> Users { get; set;}
        public DbSet<ProductOrders> ProductOrders { get; set;}

        public DbSet<Category> Categories { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // configures one-to-many relationship
            modelBuilder.Entity<User>()
                .HasMany(c => c.orders);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.products);
            //.WithOne(e => e.Category);

            modelBuilder.Entity<Product>()
            .Property(e => e.images)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            
           /* modelBuilder.Entity<Product>()
                .HasMany<Order>(s => s.orders)
                .WithMany(c => c.products)
                .UsingEntity<ProductOrders>(
                j => j
                    .HasOne(pt => pt.order)
                    .WithMany(t => t.productsOrders)
                    .HasForeignKey(pt => pt.OrderId),
                j => j
                    .HasOne(pt => pt.product)
                    .WithMany(p => p.productsOrders)
                    .HasForeignKey(pt => pt.ProductId),
                j =>
                {
                    j.HasKey(t => new { t.OrderId, t.ProductId });
                }
            );*/

            modelBuilder.Entity<ProductOrders>().HasKey(sc => new { sc.OrderId, sc.ProductId });


            modelBuilder.Entity<Order>()
            .Property(e => e.quantities)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

        }
    }
}


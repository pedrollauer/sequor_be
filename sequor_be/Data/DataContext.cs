using Microsoft.EntityFrameworkCore;
using sequor_be.Models;

namespace sequor_be.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    public DbSet<Order> Order { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Production> Production { get; set; }
    public DbSet<ProductMaterial> ProductMaterial { get; set; }
    public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductMaterials)
                .WithOne()
                .HasForeignKey(pm => pm.ProductCode);


            modelBuilder.Entity<Production>()
                .HasOne(p => p.OrderObj)  // Specify navigation property for Order
                .WithMany()
                .HasForeignKey(p => p.Order);

            modelBuilder.Entity<Order>()
            .HasOne(o => o.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductCode);

            modelBuilder.Entity<ProductMaterial>()
                .HasKey(p => new { p.ProductCode, p.MaterialCode });

            modelBuilder.Entity<ProductMaterial>()
                .HasOne(o => o.Material)
                .WithMany()
                .HasForeignKey(o => o.MaterialCode);

        }
    }
}

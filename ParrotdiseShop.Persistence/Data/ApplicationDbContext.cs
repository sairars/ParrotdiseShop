using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParrotdiseShop.Core.Models;


namespace ParrotdiseShop.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(100);

            builder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(50);

            builder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Entity<Product>()
                .Property(p => p.SKU)
                .HasMaxLength(20);

            base.OnModelCreating(builder);
        }
    }
}

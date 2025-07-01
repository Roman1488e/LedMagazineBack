using LedMagazineBack.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Context;

public class MagazineDbContext(DbContextOptions<MagazineDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Location)
            .WithOne()
            .HasForeignKey<Location>(l => l.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.ScreenSpecifications)
            .WithOne()
            .HasForeignKey<ScreenSpecifications>(s => s.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Order>()
            .HasMany(o=>o.Items)
            .WithOne()
            .HasForeignKey(o=>o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(o=>o.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Blog>()
            .HasMany(b=>b.Articles)
            .WithOne()
            .HasForeignKey(a => a.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<ScreenSpecifications> ScreenSpecifications { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Article>  Articles { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<RentTime> RentTimes { get; set; }
    public DbSet<RentTimeMultiplayer> RentTimesMultiplayer { get; set; }
    public DbSet<Blog>  Blogs { get; set; }
}
 
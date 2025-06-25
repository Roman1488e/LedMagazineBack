using LedMagazineBack.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Context;

public class MagazineDbContext(DbContextOptions<MagazineDbContext> options) : DbContext(options)
{
    public DbSet<ScreenSpecifications> ScreenSpecifications { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Article>  Articles { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<RentTime> RentTimes { get; set; }
    public DbSet<RentTimeMultiplayer> RentTimesMultiplayer { get; set; }
    public DbSet<Blog>  Blogs { get; set; }
}
 
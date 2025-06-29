using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.CartRepositories.Abstract;
using LedMagazineBack.Repositories.UserRepositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace LedMagazineBack.Seeders;

public static class DbSeeder
{
    public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
        var cartRepository = scope.ServiceProvider.GetRequiredService<ICartRepository>();
        var adminUser = await userManager.GetByUsername("admin");
        if (adminUser is not null)
            return;
        var admin = new Customer()
        {
            IsVerified = true,
            ContactNumber = "998909009090",
            OrganisationName = "Administration",
            Name = "Admin",
            Username = "admin",
            Role = "admin"
        };
        admin.PasswordHash = new PasswordHasher<Customer>().HashPassword(admin, "ynI8$56S");
        var result = await userManager.Create(admin);
        var cart = new Cart()
        {
            CustomerId = result.Id,
            Created = DateTime.UtcNow
        };
        await cartRepository.Create(cart);
    }
        
    
}


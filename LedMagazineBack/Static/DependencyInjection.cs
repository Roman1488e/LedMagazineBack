using LedMagazineBack.Repositories;
using LedMagazineBack.Repositories.Abstract;

namespace LedMagazineBack.Static;

public static class DependencyInjection
{
    public static IServiceCollection AddStaticServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRentTimeRepository, RentTimeRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<IRentTimeMultiplayerRepository, RentTimeMultiplayerRepository>();
        services.AddScoped<IScreenSpecificationRepository, ScreenSpecificationsRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        return services;
    }
}
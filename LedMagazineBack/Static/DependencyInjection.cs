using LedMagazineBack.Helpers;
using LedMagazineBack.Repositories;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Repositories.OrderRepositories;
using LedMagazineBack.Repositories.OrderRepositories.Abstract;
using LedMagazineBack.Services;
using LedMagazineBack.Services.Abstract;
using LedMagazineBack.Services.OrderServices;
using LedMagazineBack.Services.OrderServices.Absrtact;
using LedMagazineBack.Services.TelegramServices;
using LedMagazineBack.Services.TelegramServices.Abstract;
using LedMagazineBack.Services.UserServices;
using LedMagazineBack.Services.UserServices.Abstract;

namespace LedMagazineBack.Static;

public static class DependencyInjection
{
    public static IServiceCollection AddStaticRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRentTimeRepository, RentTimeRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IRentTimeMultiplayerRepository, RentTimeMultiplayerRepository>();
        services.AddScoped<IScreenSpecificationRepository, ScreenSpecificationsRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        return services;
    }

    public static IServiceCollection AddStaticServices(this IServiceCollection service)
    {
        service.AddScoped<IProductService, ProductService>();
        service.AddScoped<IRentTimeService, RentTimeService>();
        service.AddScoped<IOrderService, OrderService>();
        service.AddScoped<ICustomerService, CustomerService>();
        service.AddScoped<UserHelper>();
        service.AddScoped<IGuestService, GuestService>();
        service.AddScoped<IRentTimeMultiplayerService, RentTimeMultiplayerService>();
        service.AddScoped<ILocationService, LocationService>();
        service.AddScoped<ICartService, CartService>();
        service.AddScoped<ICartItemService, CartItemService>();
        service.AddScoped<ITelegramService, TelegramService>();
        service.AddScoped<IBlogService, BlogService>();
        service.AddScoped<IArticleService, ArticleService>();
        service.AddScoped<IOrderItemService, OrderItemService>();
        service.AddScoped<IScreenSpecificationsService, ScreenSpecificationsService>();
        service.AddScoped<IJwtService, JwtService>();
        service.AddScoped<IFileService, FileService>();
        return service;
    }
}
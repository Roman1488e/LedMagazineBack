using FluentValidation;
using LedMagazineBack.Helpers;
using LedMagazineBack.Repositories;
using LedMagazineBack.Repositories.BasicRepositories;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Repositories.BlogRepositories;
using LedMagazineBack.Repositories.BlogRepositories.Abstract;
using LedMagazineBack.Repositories.CartRepositories;
using LedMagazineBack.Repositories.CartRepositories.Abstract;
using LedMagazineBack.Repositories.OrderRepositories;
using LedMagazineBack.Repositories.OrderRepositories.Abstract;
using LedMagazineBack.Repositories.ProductRepositories;
using LedMagazineBack.Repositories.ProductRepositories.Abstract;
using LedMagazineBack.Repositories.RentTimeRepositories;
using LedMagazineBack.Repositories.RentTimeRepositories.Abstract;
using LedMagazineBack.Repositories.UserRepositories;
using LedMagazineBack.Repositories.UserRepositories.Abstract;
using LedMagazineBack.Services;
using LedMagazineBack.Services.BlogServices;
using LedMagazineBack.Services.BlogServices.Abstract;
using LedMagazineBack.Services.CartServices;
using LedMagazineBack.Services.CartServices.Abstract;
using LedMagazineBack.Services.FileServices;
using LedMagazineBack.Services.FileServices.Abstract;
using LedMagazineBack.Services.MemoryServices;
using LedMagazineBack.Services.MemoryServices.Abstract;
using LedMagazineBack.Services.OrderServices;
using LedMagazineBack.Services.OrderServices.Abstract;
using LedMagazineBack.Services.PriceServices;
using LedMagazineBack.Services.PriceServices.Abstract;
using LedMagazineBack.Services.ProductServices;
using LedMagazineBack.Services.ProductServices.Abstract;
using LedMagazineBack.Services.RentTimeServices;
using LedMagazineBack.Services.RentTimeServices.Abstract;
using LedMagazineBack.Services.TelegramServices;
using LedMagazineBack.Services.TelegramServices.Abstract;
using LedMagazineBack.Services.UserServices;
using LedMagazineBack.Services.UserServices.Abstract;
using LedMagazineBack.Validators.UserValidators;
using LedMagazineBack.Validators.UserValidators.AuthValidators;

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
        service.AddScoped<IMemoryCacheService, MemoryCacheService>();
        service.AddScoped<ICustomerService, CustomerService>();
        service.AddScoped<UserHelper>();
        service.AddScoped<IGuestService, GuestService>();
        service.AddScoped<IRentTimeMultiplayerService, RentTimeMultiplayerService>();
        service.AddScoped<ILocationService, LocationService>();
        service.AddScoped<IPriceService, PriceService>();
        service.AddScoped<ICartService, CartService>();
        service.AddScoped<ICartItemService, CartItemService>();
        service.AddScoped<ITelegramService, TelegramService>();
        service.AddScoped<IBlogService, BlogService>();
        service.AddScoped<IArticleService, ArticleService>();
        service.AddScoped<IOrderItemService, OrderItemService>();
        service.AddScoped<IScreenSpecificationsService, ScreenSpecificationsService>();
        service.AddScoped<IJwtService, JwtService>();
        service.AddScoped<IFileService, FileService>();
        service.AddValidatorsFromAssemblyContaining<RegisterValidation>();

        return service;
    }
}
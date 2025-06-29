using LedMagazineBack.Repositories.BlogRepositories.Abstract;
using LedMagazineBack.Repositories.CartRepositories.Abstract;
using LedMagazineBack.Repositories.OrderRepositories.Abstract;
using LedMagazineBack.Repositories.ProductRepositories.Abstract;
using LedMagazineBack.Repositories.RentTimeRepositories.Abstract;
using LedMagazineBack.Repositories.UserRepositories.Abstract;

namespace LedMagazineBack.Repositories.BasicRepositories.Abstract;

public interface IUnitOfWork
{
    IArticleRepository ArticleRepository { get; }
    IRentTimeRepository RentTimeRepository { get; }
    IBlogRepository BlogRepository { get; }
    ICartRepository CartRepository { get; }
    ICartItemRepository CartItemRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IGuestRepository GuestRepository { get; }
    ILocationRepository LocationRepository { get; }
    IOrderRepository OrderRepository { get; }
    IProductRepository ProductRepository { get; }
    IRentTimeMultiplayerRepository RentTimeMultiplayerRepository { get; }
    IScreenSpecificationRepository ScreenSpecificationRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
}
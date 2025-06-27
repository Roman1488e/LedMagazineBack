namespace LedMagazineBack.Repositories.Abstract;

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
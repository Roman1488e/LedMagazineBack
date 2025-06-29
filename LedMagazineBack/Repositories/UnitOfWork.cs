using LedMagazineBack.Context;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Repositories.OrderRepositories.Abstract;

namespace LedMagazineBack.Repositories;

public class UnitOfWork(
    IArticleRepository articleRepository,
    IRentTimeRepository rentTimeRepository,
    IBlogRepository blogRepository,
    ICartRepository cartRepository,
    ICartItemRepository cartItemRepository,
    ICustomerRepository customerRepository,
    IGuestRepository guestRepository,
    ILocationRepository locationRepository,
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IRentTimeMultiplayerRepository rentTimeMultiplayerRepository,
    IScreenSpecificationRepository screenSpecificationRepository,
    IOrderItemRepository orderItemRepository)
    : IUnitOfWork
{
    public IArticleRepository ArticleRepository { get; } = articleRepository;
    public IRentTimeRepository RentTimeRepository { get; } = rentTimeRepository;
    public IBlogRepository BlogRepository { get; } = blogRepository;
    public ICartRepository CartRepository { get; } = cartRepository;
    public ICartItemRepository CartItemRepository { get; } = cartItemRepository;
    public ICustomerRepository CustomerRepository { get; } = customerRepository;
    public IGuestRepository GuestRepository { get; } = guestRepository;
    public ILocationRepository LocationRepository { get; } = locationRepository;
    public IOrderRepository OrderRepository { get; } = orderRepository;
    public IProductRepository ProductRepository { get; } = productRepository;
    public IRentTimeMultiplayerRepository RentTimeMultiplayerRepository { get; } = rentTimeMultiplayerRepository;
    public IScreenSpecificationRepository ScreenSpecificationRepository { get; } = screenSpecificationRepository;
    public IOrderItemRepository OrderItemRepository { get; } = orderItemRepository;
}
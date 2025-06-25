using LedMagazineBack.Context;
using LedMagazineBack.Repositories.Abstract;

namespace LedMagazineBack.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MagazineDbContext _context;

    public UnitOfWork(MagazineDbContext context, IArticleRepository articleRepository, IRentTimeRepository rentTimeRepository, IBlogRepository blogRepository, ICartRepository cartRepository, ICartItemRepository cartItemRepository, ICustomerRepository customerRepository, IGuestRepository guestRepository, ILocationRepository locationRepository, IOrderRepository orderRepository, IProductRepository productRepository, IRentTimeMultiplayerRepository rentTimeMultiplayerRepository, IScreenSpecificationRepository screenSpecificationRepository)
    {
        _context = context;
        ArticleRepository = articleRepository;
        RentTimeRepository = rentTimeRepository;
        BlogRepository = blogRepository;
        CartRepository = cartRepository;
        CartItemRepository = cartItemRepository;
        CustomerRepository = customerRepository;
        GuestRepository = guestRepository;
        LocationRepository = locationRepository;
        OrderRepository = orderRepository;
        ProductRepository = productRepository;
        RentTimeMultiplayerRepository = rentTimeMultiplayerRepository;
        ScreenSpecificationRepository = screenSpecificationRepository;
    }

    public IArticleRepository ArticleRepository { get; }
    public IRentTimeRepository RentTimeRepository { get; }
    public IBlogRepository BlogRepository { get; }
    public ICartRepository CartRepository { get; }
    public ICartItemRepository CartItemRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public IGuestRepository GuestRepository { get; }
    public ILocationRepository LocationRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IRentTimeMultiplayerRepository RentTimeMultiplayerRepository { get; }
    public IScreenSpecificationRepository ScreenSpecificationRepository { get; }
}
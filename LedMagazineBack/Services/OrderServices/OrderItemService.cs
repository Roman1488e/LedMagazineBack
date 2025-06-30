using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Models.OrderModels.CreationModels;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.MemoryServices.Abstract;
using LedMagazineBack.Services.OrderServices.Abstract;
using LedMagazineBack.Services.PriceServices.Abstract;
using LedMagazineBack.Services.TelegramServices.Abstract;

namespace LedMagazineBack.Services.OrderServices;

public class OrderItemService(IUnitOfWork unitOfWork, UserHelper userHelper, ITelegramService telegramService, IMemoryCacheService cache, IPriceService price) : IOrderItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITelegramService _telegramService = telegramService;
    private readonly UserHelper _userHelper = userHelper;
    private readonly IMemoryCacheService _cache = cache;
    private readonly IPriceService _price = price;
    private const string Key = "OrdersItems";

    public async Task<List<OrderItem>> GetAll()
    {
        var cached = _cache.GetCache<List<OrderItem>>(Key);
        if(cached is not null && cached.Count > 0 )
            return cached;
        await Set();
        var orderItems = await _unitOfWork.OrderItemRepository.GetAll();
        return orderItems;
    }

    public async Task<List<OrderItem>> GetByOrderId(Guid orderId)
    {
        var cached = _cache.GetCache<List<OrderItem>>(Key);
        if (cached is not null && cached.Count > 0)
        {
            var items = cached.Where(x=>x.OrderId == orderId).ToList();
            return items;
        }
        await Set();
        var orderItems = await _unitOfWork.OrderItemRepository.GetByOrderId(orderId);
        return orderItems;
    }

    public async Task<List<OrderItem>> GetByProductName(string productName)
    {
        var cached = _cache.GetCache<List<OrderItem>>(Key);
        if (cached is not null && cached.Count > 0)
        {
            var items = cached.Where(x => x.ProductName == productName).ToList();
            return items;
        }
        await Set();
        var orderItems = await _unitOfWork.OrderItemRepository.GetByProductName(productName);
        return orderItems;
    }

    public async Task<OrderItem> GetById(Guid id)
    {
        var cached = _cache.GetCache<List<OrderItem>>(Key);
        if (cached is not null && cached.Count > 0)
        {
            var item = cached.SingleOrDefault(x => x.OrderId == id);
            if(item is null)
                throw new Exception($"OrderItem with id {id} not found");
            return item;
        }
        await Set();
        var orderItem = await _unitOfWork.OrderItemRepository.GetById(id);
        return orderItem;
    }

    public async Task<OrderItem> Create(CreateOrderItemModel model)
    {
        var product = await _unitOfWork.ProductRepository.GetById(model.ProductId);
        var order = await _unitOfWork.OrderRepository.GetById(model.OrderId);
        if(order.Items.Count != 0)
            throw new Exception("This order already has order items. Create new one first");
        var orderItem = new OrderItem()
        {
            ImageUrl = product.ImageUrl,
            ProductName = product.Name,
            OrderId = model.OrderId,
            ProductId = product.Id,
        }; 
        await _unitOfWork.OrderItemRepository.Create(orderItem);
        var rentTime = new RentTime()
        {
            OrderItemId = orderItem.Id,
            RentSeconds = model.RentSeconds,
            RentMonths = model.RentMonths,
            CreatedDate = DateTime.UtcNow,
            EndOfRentDate = DateTime.UtcNow.AddMonths(model.RentMonths)
        };
        await _unitOfWork.RentTimeRepository.Create(rentTime);
        orderItem.Price = await _price.GeneratePrice(product.Id, model.RentMonths, model.RentSeconds);
        await _unitOfWork.OrderItemRepository.Update(orderItem);
        order.TotalPrice += orderItem.Price;
        await _unitOfWork.OrderRepository.Update(order);
        await _telegramService.GenerateMessageAsync(orderItem.OrderId);
        await Set();
        return orderItem;
    }

    public async Task<OrderItem> Delete(Guid id)
    {
        var orderItem = await _unitOfWork.OrderItemRepository.Delete(id);
        await Set();
        return orderItem;
    }
    
    private async Task Set()
    {
        var customers = await _unitOfWork.OrderItemRepository.GetAll();
        _cache.SetCache(Key,customers);
    }
}
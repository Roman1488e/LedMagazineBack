using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Models.Order;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.OrderServices.Absrtact;
using LedMagazineBack.Services.TelegramServices.Abstract;

namespace LedMagazineBack.Services.OrderServices;

public class OrderItemService(IUnitOfWork unitOfWork, UserHelper userHelper, ITelegramService telegramService) : IOrderItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITelegramService _telegramService = telegramService;
    private readonly UserHelper _userHelper = userHelper;

    public async Task<List<OrderItem>> GetAll()
    {
        var orderItems = await _unitOfWork.OrderItemRepository.GetAll();
        return orderItems;
    }

    public async Task<List<OrderItem>> GetByOrderId(Guid orderId)
    {
        var orderItems = await _unitOfWork.OrderItemRepository.GetByOrderId(orderId);
        return orderItems;
    }

    public async Task<List<OrderItem>> GetByProductName(string productName)
    {
        var orderItems = await _unitOfWork.OrderItemRepository.GetByProductName(productName);
        return orderItems;
    }

    public async Task<OrderItem> GetById(Guid id)
    {
        var orderItem = await _unitOfWork.OrderItemRepository.GetById(id);
        return orderItem;
    }

    public async Task<OrderItem> Create(CreateOrderItemModel model)
    {
        var product = await _unitOfWork.ProductRepository.GetById(model.ProductId);
        var order = await _unitOfWork.OrderRepository.GetById(model.OrderId);
        var orderItem = new OrderItem()
        {
            ImageUrl = product.ImageUrl,
            ProductName = product.Name,
            OrderId = model.OrderId,
            ProductId = product.Id,
        };
        var createdOrderItem = await _unitOfWork.OrderItemRepository.Create(orderItem);
        var rentTime = new RentTime()
        {
            OrderItemId = createdOrderItem.Id,
            RentSeconds = model.RentSeconds,
            RentMonths = model.RentMonths,
            CreatedDate = DateTime.UtcNow,
            EndOfRentDate = DateTime.UtcNow.AddMonths(model.RentMonths)
        };
        await _unitOfWork.RentTimeRepository.Create(rentTime);
        createdOrderItem.Price = product.BasePrice * (decimal)product.RentTimeMultiplayer.MonthsDifferenceMultiplayer + product.BasePrice * (decimal)product.RentTimeMultiplayer.SecondsDifferenceMultiplayer;
        await _unitOfWork.OrderItemRepository.Update(createdOrderItem);
        order.TotalPrice += createdOrderItem.Price;
        await _unitOfWork.OrderRepository.Update(order);
        await _telegramService.GenerateMessageAsync(orderItem.OrderId);
        return createdOrderItem;
    }

    public async Task<OrderItem> Delete(Guid id)
    {
        var orderItem = await _unitOfWork.OrderItemRepository.Delete(id);
        return orderItem;
    }
}
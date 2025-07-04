using LedMagazineBack.Entities;
using LedMagazineBack.Models.OrderModels.CreationModels;

namespace LedMagazineBack.Services.OrderServices.Abstract;

public interface IOrderItemService
{
    public Task<List<OrderItem>> GetAll();
    public Task<List<OrderItem>> GetByOrderId(Guid orderId);
    public Task<List<OrderItem>> GetByProductName(string productName);
    public Task<OrderItem>  GetById(Guid id);
    public Task<OrderItem> Create(CreateOrderItemModel model);
    public Task<OrderItem> Delete(Guid id);
}
using LedMagazineBack.Entities;
using LedMagazineBack.Models.OrderModels.CreationModels;
using LedMagazineBack.Models.OrderModels.FilterModel;
using LedMagazineBack.Models.OrderModels.UpdateModels;

namespace LedMagazineBack.Services.OrderServices.Abstract;

public interface IOrderService
{
    public Task<List<Order>> GetAll(FilterOrdersModel model);
    public Task<Order> GetById(Guid id);
    public Task<Order> Create(CreateOrderModel model);
    public Task<Order> CreateFromCart(CreateOrderModel model);
    public Task<Order> CreateFromCart();
    public Task<Order?> GetByOrderNumber(uint orderNumber);
    public Task<Order> Create();
    public Task<Order> Accept(Guid id);
    public Task<Order> Cancel(Guid id);
    public Task<Order> Delete(Guid id);
    public Task<Order> Update(Guid id, UpdateOrderModel model);
}
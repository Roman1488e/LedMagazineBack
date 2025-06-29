using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.Order;
using LedMagazineBack.Models.Order.FilterModel;

namespace LedMagazineBack.Services.OrderServices.Absrtact;

public interface IOrderService
{
    public Task<List<Order>> GetAll(FilterOrdersModel model);
    public Task<Order> GetById(Guid id);
    public Task<Order> Create(CreateOrderModel model);
    public Task<Order> CreateFromCart(CreateOrderModel model);
    public Task<Order> CreateFromCart();
    public Task<Order> Create();
    public Task<Order> Accept(Guid id);
    public Task<Order> Cancel(Guid id);
    public Task<Order> Delete(Guid id);
    public Task<Order> Update(Guid id, UpdateOrderModel model);
}
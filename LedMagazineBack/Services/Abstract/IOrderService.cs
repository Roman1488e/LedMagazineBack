using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services.Abstract;

public interface IOrderService
{
    public Task<List<Order>> GetAll();
    public Task<Order> GetById(Guid id);
    public Task<Order> Create(CreateOrderModel model);
    public Task<Order> Create();
    public Task<Order> Accept(Guid id);
    public Task<Order> Cancel(Guid id);
    public Task<Order> Delete(Guid id);
    public Task<Order> Update(Guid id, UpdateOrderModel model);
}
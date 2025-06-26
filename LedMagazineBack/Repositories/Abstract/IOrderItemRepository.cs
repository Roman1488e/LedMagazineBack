using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface IOrderItemRepository : IBaseRepository<OrderItem>
{
    public Task<OrderItem> GetById(Guid id);
    public Task<List<OrderItem>> GetByOrderId(Guid orderId);
    public Task<List<OrderItem>> GetByProductName(string productName);
    public Task<List<OrderItem>> GetAll();
}
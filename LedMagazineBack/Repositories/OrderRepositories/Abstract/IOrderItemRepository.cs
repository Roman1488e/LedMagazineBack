using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.OrderRepositories.Abstract;

public interface IOrderItemRepository : IBaseRepository<OrderItem>
{
    public Task<OrderItem> GetById(Guid id);
    public Task<List<OrderItem>> GetByOrderId(Guid orderId);
    public Task<List<OrderItem>> GetByProductName(string productName);
    public Task<List<OrderItem>> GetAll();
}
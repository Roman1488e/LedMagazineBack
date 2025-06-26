using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface IOrderRepository : IBaseRepository<Order>
{
    public Task<Order> GetById(Guid id);
    public Task<List<Order>> GetActive();
    public Task<List<Order>> GetAll();
    public Task<List<Order>> GetPrimary();
    public Task<List<Order>> GetByOrgName(string orgName);
    public Task<List<Order>> GetFiltered(DateTime? startDate, DateTime? endDate, decimal minPrice, decimal maxPrice);
    public Task<List<Order>> GetByUserId(Guid userId);
    public Task<List<Order>> GetNotAccepted();
    public Task<List<Order>> GetAccepted();
    public Task<List<Order>> GetByProductName(string productName);
}
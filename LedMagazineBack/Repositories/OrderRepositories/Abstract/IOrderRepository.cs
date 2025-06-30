using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.OrderRepositories.Abstract;

public interface IOrderRepository : IBaseRepository<Order>
{
    public Task<Order> GetById(Guid id);
    public Task<List<Order>> GetAll();
    public Task<Order?> GetByOrderNumber(uint orderNumber);
    public Task<List<Order>> GetAll(
        string? productName ,
        string? orgName,
        bool? isAccepted,
        bool? isActive,
        bool? isPrimary,
        DateTime? startDate,
        DateTime? endDate,
        decimal? minPrice,
        decimal? maxPrice,
        int page,
        int pageSize);
    public Task<List<Order>> GetByUserId(Guid userId);
}
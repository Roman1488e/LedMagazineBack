using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.CartRepositories.Abstract;

public interface ICartRepository : IBaseRepository<Cart>
{
    public Task<List<Cart>> GetAll();
    public Task<Cart> GetById(Guid id);
    public Task<Cart?> GetByCustomerId(Guid customerId);
    public Task<Cart?> GetBySessionId(Guid sessionId);
    public Task<List<Cart>> GetByDate(DateTime startDate, DateTime endDate);
}
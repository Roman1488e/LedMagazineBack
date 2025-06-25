using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface ICartRepository : IBaseRepository<Cart>
{
    public Task<List<Cart>> GetAll();
    public Task<Cart> GetById(Guid id);
    public Task<Cart> GetByCustomerId(Guid customerId);
    public Task<List<Cart>> GetByDate(DateTime startDate, DateTime endDate);
}
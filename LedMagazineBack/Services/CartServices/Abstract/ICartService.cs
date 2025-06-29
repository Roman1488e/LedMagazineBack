using LedMagazineBack.Entities;

namespace LedMagazineBack.Services.CartServices.Abstract;

public interface ICartService
{
    public Task<List<Cart>> GetAll();
    public Task<Cart> GetById(Guid id);
    public Task<Cart> GetBySessionId();
    public Task<Cart> GetByCustomerId();
    public Task<Cart> Delete(Guid id);
    public Task<Cart> DeleteBySessionId(Guid sessionId);
    public Task<Cart> CreateForGuest();
    public Task<Cart> CreateForCustomer();
}
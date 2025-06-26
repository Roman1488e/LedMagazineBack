using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services.Abstract;

public interface ICartService
{
    public Task<List<Cart>> GetAll();
    public Task<Cart> GetById(Guid id);
    public Task<Cart> GetBySessionId(Guid sessionId);
    public Task<Cart> GetByCustomerId();
    public Task<Cart> SwitchToClient(Guid id);
    public Task<Cart> Delete(Guid id);
    public Task<Cart> DeleteBySessionId(Guid sessionId);
    public Task<Cart> Create();
    public Task<Cart> Create(CreateCartModel cartModel);
}
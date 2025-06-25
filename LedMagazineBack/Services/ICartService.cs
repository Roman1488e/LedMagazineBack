using LedMagazineBack.Entities;

namespace LedMagazineBack.Services;

public interface ICartService
{
    public Task<List<Cart>> GetAll();
    public Task<Cart> GetById(Guid id);
    public Task<Cart> GetBySessionId(Guid sessionId);
    public Task<Cart> SwitchToClient(Guid sessionId, Guid clientId);
    public Task<Cart> Delete(Guid id);
    public Task<Cart> DeleteBySessionId(Guid sessionId);
}
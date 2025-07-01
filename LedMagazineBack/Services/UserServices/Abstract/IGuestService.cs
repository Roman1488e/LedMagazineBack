using LedMagazineBack.Entities;

namespace LedMagazineBack.Services.UserServices.Abstract;

public interface IGuestService
{
    public Task<string> Create();
    public Task<List<Guest>> GetAll();
    public Task<Guest> GetById(Guid id);
    public Task<Guest> GetBySessionId(Guid sessionId);
    public Task<Guest> DeleteBySessionId();
    public Task ClearAll();
    public Task<Guest> DeleteBySessionId(Guid sessionId);
}
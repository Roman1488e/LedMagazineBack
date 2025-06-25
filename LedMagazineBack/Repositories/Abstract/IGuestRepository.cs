using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface IGuestRepository : IBaseRepository<Guest>
{
    public Task<List<Guest>> GetAll();
    public Task<Guest> GetById(Guid id);
    public Task<Guest> GetBySessionId(Guid sessionId);
    public Task<List<Guest>> GetByDate(DateTime startDate, DateTime endDate);
}
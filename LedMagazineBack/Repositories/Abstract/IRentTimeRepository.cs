using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface IRentTimeRepository : IBaseRepository<RentTime>
{
    public Task<List<RentTime>> GetAll();
    public Task<RentTime> GetById(Guid id);
    public Task<RentTime> GetByCartItemId(Guid cartItemId);
    public Task<RentTime> GetByOrderItemId(Guid orderItemId);
    public Task<List<RentTime>> GetByMonths(int minmonth, int maxmonth);
    public Task<List<RentTime>> GetBySeconds(int minsec, int maxsec);
}
using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface IRentTimeRepository : IBaseRepository<RentTime>
{
    public Task<List<RentTime>> GetAll();
    public Task<RentTime> GetById(Guid id);
    public Task<RentTime> GetByOrderId(Guid orderId);
    public Task<List<RentTime>> GetByMonths(int minmonth, int maxmonth);
    public Task<List<RentTime>> GetBySeconds(int minsec, int maxsec);
}
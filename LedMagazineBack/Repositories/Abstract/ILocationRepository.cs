using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface ILocationRepository : IBaseRepository<Location>
{
    public Task<List<Location>> GetAll();
    public Task<Location> GetById(Guid id);
    public Task<Location> GetByProductId(Guid productId);
    public Task<List<Location>> GetByDistrict(string district);
}
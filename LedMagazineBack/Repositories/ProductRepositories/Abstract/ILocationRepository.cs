using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.ProductRepositories.Abstract;

public interface ILocationRepository : IBaseRepository<Location>
{
    public Task<List<Location>> GetAll();
    public Task<Location> GetById(Guid id);
    public Task<Location> GetByProductId(Guid productId);
    public Task<List<Location>> GetByDistrict(string district);
}
using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services.Abstract;

public interface ILocationService
{
    public Task<Location> GetById(Guid id);
    public Task<List<Location>> GetAll();
    public Task<Location> GetByProductId(Guid productId);
    public Task<Location> Create(CreateLocationModel model);
    public Task<Location> Delete(Guid id);
    public Task<Location> Update(Guid id, UpdateLocationModel model);
}
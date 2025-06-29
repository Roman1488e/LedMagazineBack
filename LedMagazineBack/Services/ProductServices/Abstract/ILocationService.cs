using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.ProductModels.CreationModels;
using LedMagazineBack.Models.ProductModels.UpdateModels;

namespace LedMagazineBack.Services.ProductServices.Abstract;

public interface ILocationService
{
    public Task<Location> GetById(Guid id);
    public Task<List<Location>> GetAll();
    public Task<Location> GetByProductId(Guid productId);
    public Task<Location> Create(CreateLocationModel model);
    public Task<Location> Delete(Guid id);
    public Task<Location> Update(Guid id, UpdateLocationModel model);
}
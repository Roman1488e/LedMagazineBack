using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services.Abstract;

public interface IScreenSpecificationsService
{
    public Task<ScreenSpecifications> GetById(Guid id);
    public Task<ScreenSpecifications> GetByProductId(Guid productId);
    public Task<ScreenSpecifications> Create(CreateScreenSpecsModel model);
    public Task<ScreenSpecifications> Delete(Guid id);
    public Task<ScreenSpecifications> Update(Guid id, UpdateScreenSpecsModel model);
    public Task<List<ScreenSpecifications>> GetAll();
}
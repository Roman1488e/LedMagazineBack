using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services;

public interface IScreenSpecificationsService
{
    public Task<ScreenSpecifications> GetById(Guid id);
    public Task<ScreenSpecifications> GetByProductId(Guid productId);
    public Task<ScreenSpecifications> Delete(Guid id);
    public Task<ScreenSpecifications> Update(UpdateScreenSpecsModel model);
    public Task<List<ScreenSpecifications>> GetAll();
}
using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface IScreenSpecificationRepository : IBaseRepository<ScreenSpecifications>
{
    public Task<List<ScreenSpecifications>> GetAll();
    public Task<ScreenSpecifications> GetByProductId(Guid productId);
    public Task<ScreenSpecifications> GetById(Guid id);
    public Task<List<ScreenSpecifications>> GetByScreenSize(string screenSize);
    public Task<List<ScreenSpecifications>> GetByResolution(string resolution);
    public Task<List<ScreenSpecifications>> GetByScreenType(string screenType);
}
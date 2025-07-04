using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.RentTimeRepositories.Abstract;

public interface IRentTimeMultiplayerRepository : IBaseRepository<RentTimeMultiplayer>
{
    public Task<List<RentTimeMultiplayer>> GetAll();
    public Task<RentTimeMultiplayer> GetById(Guid id);
    public Task<RentTimeMultiplayer> GetByСoefficient(float coefficient);
    public Task<RentTimeMultiplayer> GetByСoefficient(float min, float max);
    public Task<RentTimeMultiplayer> GetByProductId(Guid productId);
}
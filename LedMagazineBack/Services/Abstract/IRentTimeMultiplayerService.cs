using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services.Abstract;

public interface IRentTimeMultiplayerService
{
    public Task<List<RentTimeMultiplayer>> GetAll();
    public Task<RentTimeMultiplayer> GetById(Guid id);
    public Task<RentTimeMultiplayer> GetByProductId(Guid productId);
    public Task<RentTimeMultiplayer> Update(Guid id, UpdateRentTimeMultModel model);
    public Task<RentTimeMultiplayer> Create(CreateRentTimeMulModel model);
}
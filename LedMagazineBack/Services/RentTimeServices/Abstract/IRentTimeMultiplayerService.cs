using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.RentTimeModels.CreationModels;
using LedMagazineBack.Models.RentTimeModels.UpdateModels;

namespace LedMagazineBack.Services.RentTimeServices.Abstract;

public interface IRentTimeMultiplayerService
{
    public Task<List<RentTimeMultiplayer>> GetAll();
    public Task<RentTimeMultiplayer> GetById(Guid id);
    public Task<RentTimeMultiplayer> GetByProductId(Guid productId);
    public Task<RentTimeMultiplayer> Delete(Guid id);
    public Task<RentTimeMultiplayer> Update(Guid id, UpdateRentTimeMultModel model);
    public Task<RentTimeMultiplayer> Create(CreateRentTimeMulModel model);
}
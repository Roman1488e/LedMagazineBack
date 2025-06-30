using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.RentTimeModels.CreationModels;
using LedMagazineBack.Models.RentTimeModels.UpdateModels;

namespace LedMagazineBack.Services.RentTimeServices.Abstract;

public interface IRentTimeService
{
    public Task<List<RentTime>> GetAll();
    public Task<RentTime> GetById(Guid id);
    public Task<RentTime> GetByCartItemId(Guid id);
    public Task<RentTime> Delete(Guid id);
    public Task<RentTime> GetByOrderItemId(Guid id);
    /*public Task<RentTime> Create(CreateRentTimeModel rentTime);*/
    public Task<RentTime> Update(Guid id, UpdateRentTimeModel model);
}
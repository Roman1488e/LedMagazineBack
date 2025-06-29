using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services.Abstract;

public interface IRentTimeService
{
    public Task<List<RentTime>> GetAll();
    public Task<RentTime> GetById(Guid id);
    public Task<RentTime> GetByCartItemId(Guid id);
    public Task<RentTime> Delete(Guid id);
    public Task<RentTime> GetByOrderItemId(Guid id);
    public Task<RentTime> Create(CreateRentTimeModel rentTime);
    public Task<RentTime> Update(Guid id, UpdateRentTimeModel model);
}
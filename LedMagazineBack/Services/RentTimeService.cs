using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class RentTimeService(IUnitOfWork unitOfWork) : IRentTimeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<RentTime>> GetAll()
    {
        var rentTime = await _unitOfWork.RentTimeRepository.GetAll();
        return rentTime;
    }

    public async Task<RentTime> GetById(Guid id)
    {
        var rentTime = await _unitOfWork.RentTimeRepository.GetById(id);
        return rentTime;
    }

    public async Task<RentTime> Create(CreateRentTimeModel model)
    {
        var rentTime = new RentTime()
        {
            CreatedDate = DateTime.UtcNow,
            RentMonths = model.RentMonths,
            RentSeconds = model.RentSeconds,
        };
        if(model.CartItemId is not null)
            rentTime.CartItemId = model.CartItemId;
        rentTime.EndOfRentDate = rentTime.CreatedDate.AddMonths(rentTime.RentMonths);
        var result = await _unitOfWork.RentTimeRepository.Create(rentTime);
        return result;
    }

    public async Task<RentTime> Update(Guid id, UpdateRentTimeModel model)
    {
        var rentTime = await _unitOfWork.RentTimeRepository.GetById(id);
        var check = false;
        if (model.RentMonths > 0)
        {
            rentTime.RentMonths = model.RentMonths;
            check = true;
        }

        if (model.RentSeconds > 0)
        {
            rentTime.RentSeconds = model.RentSeconds;
            check = true;
        }
        if(check)
            await _unitOfWork.RentTimeRepository.Update(rentTime);
        return rentTime;
    }
}
using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class RentTimeMultiplayerService(IUnitOfWork unitOfWork) : IRentTimeMultiplayerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<List<RentTimeMultiplayer>> GetAll()
    {
        var result = await _unitOfWork.RentTimeMultiplayerRepository.GetAll();
        return result;
    }

    public async Task<RentTimeMultiplayer> GetById(Guid id)
    {
        var result = await _unitOfWork.RentTimeMultiplayerRepository.GetById(id);
        return result;
    }

    public async Task<RentTimeMultiplayer> GetByProductId(Guid productId)
    {
        var result = await _unitOfWork.RentTimeMultiplayerRepository.GetByProductId(productId);
        return result;
    }

    public async Task<RentTimeMultiplayer> Update(Guid id, UpdateRentTimeMultModel model)
    {
        var existingRentTimeMult = await _unitOfWork.RentTimeMultiplayerRepository.GetById(id);
        var check = false;
        if (model.MonthsDifferenceMultiplayer > 0)
        {
            existingRentTimeMult.MonthsDifferenceMultiplayer = model.MonthsDifferenceMultiplayer;
            check = true;
        }

        if (model.SecondsDifferenceMultiplayer > 0)
        {
            existingRentTimeMult.SecondsDifferenceMultiplayer = model.SecondsDifferenceMultiplayer;
            check = true;
        }
        if (check)
            await  _unitOfWork.RentTimeMultiplayerRepository.Update(existingRentTimeMult);
        return existingRentTimeMult;
    }

    public async Task<RentTimeMultiplayer> Create(CreateRentTimeMulModel model)
    {
        var newRentTimeMult = new RentTimeMultiplayer()
        {
            MonthsDifferenceMultiplayer = model.MonthsDifferenceMultiplayer,
            SecondsDifferenceMultiplayer = model.SecondsDifferenceMultiplayer,
            ProductId = model.ProductId
        };
        var result = await _unitOfWork.RentTimeMultiplayerRepository.Create(newRentTimeMult);
        return result;
    }
}
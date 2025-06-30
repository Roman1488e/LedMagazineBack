using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.PriceServices.Abstract;

namespace LedMagazineBack.Services.PriceServices;

public class PriceService(IUnitOfWork unitOfWork) : IPriceService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<decimal> GeneratePrice(Guid productId, byte months, byte seconds)
    {
        var product = await _unitOfWork.ProductRepository.GetById(productId);

        if (product is null || product.RentTimeMultiplayer is null)
            throw new Exception("Invalid product or RentTimeMultiplier not set");

        if (months < 1 || seconds < 5)
            throw new Exception("Minimum rent duration is 1 month and 5 seconds");

        decimal basePrice = product.BasePrice;
        decimal totalPrice = basePrice;
        
        int extraMonths = months - 1;
        if (extraMonths > 0)
        {
            decimal monthIncrement = basePrice * (decimal)(product.RentTimeMultiplayer.MonthsDifferenceMultiplayer - 1);
            totalPrice += extraMonths * monthIncrement;
        }
        
        int extraSeconds = seconds - 5;
        if (extraSeconds > 0)
        {
            int secondSteps = extraSeconds / 5;
            decimal secondIncrement = basePrice * (decimal)(product.RentTimeMultiplayer.SecondsDifferenceMultiplayer - 1);
            totalPrice += secondSteps * secondIncrement;
        }

        return totalPrice;
    }


}
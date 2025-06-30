using LedMagazineBack.Entities;

namespace LedMagazineBack.Services.PriceServices.Abstract;

public interface IPriceService
{
    public Task<decimal> GeneratePrice(Guid productId,byte months, byte seconds);
}
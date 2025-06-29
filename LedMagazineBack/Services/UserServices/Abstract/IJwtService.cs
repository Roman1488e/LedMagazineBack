using LedMagazineBack.Entities;

namespace LedMagazineBack.Services.UserServices.Abstract;

public interface IJwtService
{
    public string GenerateTokenForCustomer(Customer customer);
    public string GenerateTokenForGuest(Guest guest);
}
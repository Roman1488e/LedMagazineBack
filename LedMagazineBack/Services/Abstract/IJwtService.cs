using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LedMagazineBack.Entities;
using Microsoft.IdentityModel.Tokens;

namespace LedMagazineBack.Services.Abstract;

public interface IJwtService
{
    public string GenerateTokenForCustomer(Customer customer);
    public string GenerateTokenForGuest(Guest guest);
}
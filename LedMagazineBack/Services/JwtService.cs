using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Services.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace LedMagazineBack.Services;

public class JwtService(IConfiguration configuration) : IJwtService
{
    private JwtParameters JwtParam { get; set; } = configuration.GetSection("JwtParameters")
        .Get<JwtParameters>()!;

    public string GenerateTokenForCustomer(Customer customer)
    {
        var key = System.Text.Encoding.UTF32.GetBytes(JwtParam.Key);
        var signingKey = new SigningCredentials(new SymmetricSecurityKey(key), "HS256");

        var claims = new List<Claim>()
        { 
            new Claim(ClaimTypes.NameIdentifier,customer.Id.ToString()),
            new Claim(ClaimTypes.Role,customer.Role!)
        };


        var security = new JwtSecurityToken(issuer: JwtParam.Issuer,
            audience: JwtParam.Audience, signingCredentials: signingKey,
            claims: claims, expires: DateTime.Now.AddSeconds(10));


        var token = new JwtSecurityTokenHandler()
            .WriteToken(security);

        return token;
    }
    
    public string GenerateTokenForGuest(Guest guest)
    {
        var key = System.Text.Encoding.UTF32.GetBytes(JwtParam.Key);
        var signingKey = new SigningCredentials(new SymmetricSecurityKey(key), "HS256");

        var claims = new List<Claim>()
        { 
            new Claim(ClaimTypes.NameIdentifier,guest.SessionId.ToString()),
            new Claim(ClaimTypes.Role,guest.Role!)
        };


        var security = new JwtSecurityToken(issuer: JwtParam.Issuer,
            audience: JwtParam.Audience, signingCredentials: signingKey,
            claims: claims, expires: DateTime.Now.AddSeconds(10));


        var token = new JwtSecurityTokenHandler()
            .WriteToken(security);

        return token;
    }
    
}
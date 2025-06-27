using System.Security.Claims;

namespace LedMagazineBack.Helpers;

public class UserHelper(IHttpContextAccessor contextAccessor)
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public string GetRole()
    {
        var role = _contextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.Role);
        if (role == null)
            throw new Exception("Role not found");
        return role;
    }

    public Guid GetUserId()
    {
        var userId = Guid.Parse(_contextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));
        return userId;

    }
}
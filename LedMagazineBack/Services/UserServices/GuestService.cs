using LedMagazineBack.Constants;
using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.UserServices.Abstract;

namespace LedMagazineBack.Services.UserServices;

public class GuestService(IUnitOfWork unitOfWork, IJwtService jwtService, UserHelper userHelper) : IGuestService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtService _jwtService = jwtService;
    private readonly UserHelper _userHelper = userHelper;
    private readonly RolesConstants  _rolesConstants = new RolesConstants();

    public async Task<string> Create()
    {
        var guest = new Guest()
        {
            SessionId = Guid.NewGuid(),
            Created = DateTime.UtcNow,
            Role = _rolesConstants.Guest,
        };
        var guestDb = await _unitOfWork.GuestRepository.Create(guest);
        var cart = new Cart()
        {
            SessionId = guest.SessionId,
            Created = DateTime.UtcNow
        };
        await _unitOfWork.CartRepository.Create(cart);
        return _jwtService.GenerateTokenForGuest(guestDb);
    }

    public async Task<List<Guest>> GetAll()
    {
        var guests = await _unitOfWork.GuestRepository.GetAll();
        return guests;
    }

    public async Task<Guest> GetById(Guid id)
    {
        var guest = await _unitOfWork.GuestRepository.GetById(id);
        return guest;
    }

    public async Task<Guest> GetBySessionId(Guid sessionId)
    {
        var guest = await _unitOfWork.GuestRepository.GetBySessionId(sessionId);
        return guest;
    }

    public async Task<Guest> DeleteBySessionId(Guid sessionId)
    {
        var guest = await _unitOfWork.GuestRepository.GetBySessionId(sessionId);
        await _unitOfWork.GuestRepository.Delete(guest.Id);
        return guest;
    }

    public async Task<Guest> DeleteBySessionId()
    {
        var guest = await _unitOfWork.GuestRepository.GetBySessionId(_userHelper.GetUserId());
        await _unitOfWork.GuestRepository.Delete(guest.Id);
        return guest;
    }
    
}
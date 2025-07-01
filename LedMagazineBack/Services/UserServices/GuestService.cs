using LedMagazineBack.Constants;
using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.MemoryServices.Abstract;
using LedMagazineBack.Services.UserServices.Abstract;
using Microsoft.Extensions.Caching.Memory;

namespace LedMagazineBack.Services.UserServices;

public class GuestService(IUnitOfWork unitOfWork, IJwtService jwtService, UserHelper userHelper, IMemoryCacheService cache) : IGuestService
{
    private readonly IMemoryCacheService _cache = cache;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtService _jwtService = jwtService;
    private readonly UserHelper _userHelper = userHelper;
    private const string Key = "Guests";
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
        await Set();
        return _jwtService.GenerateTokenForGuest(guestDb);
    }

    public async Task<List<Guest>> GetAll()
    {
        var cached = _cache.GetCache<List<Guest>>(Key);
        if(cached is not null && cached.Count > 0)
            return cached;
        await Set();
        var guests = await _unitOfWork.GuestRepository.GetAll();
        return guests;
    }

    public async Task<Guest> GetById(Guid id)
    {
        var cached = _cache.GetCache<List<Guest>>(Key);
        if (cached is not null && cached.Count > 0)
        {
            var cachedGuest = cached.SingleOrDefault(x => x.Id == id);
            if(cachedGuest is null)
                throw new Exception("Guest not found");
            return cachedGuest;
        }
        var guest = await _unitOfWork.GuestRepository.GetById(id);
        return guest;
    }

    public async Task<Guest> GetBySessionId(Guid sessionId)
    {
        var cached = _cache.GetCache<List<Guest>>(Key);
        if (cached is not null && cached.Count > 0)
        {
            var cachedGuest  = cached.SingleOrDefault(x => x.SessionId == sessionId);
            if(cachedGuest is null)
                throw new Exception("Guest not found");
            return cachedGuest;
        }
        var guest = await _unitOfWork.GuestRepository.GetBySessionId(sessionId);
        return guest;
    }

    public async Task ClearAll()
    {
        await _unitOfWork.GuestRepository.ClearAll();
        await Set();
    }

    public async Task<Guest> DeleteBySessionId(Guid sessionId)
    {
        var guest = await _unitOfWork.GuestRepository.GetBySessionId(sessionId);
        await _unitOfWork.GuestRepository.Delete(guest.Id);
        await Set();
        return guest;
    }

    public async Task<Guest> DeleteBySessionId()
    {
        var guest = await _unitOfWork.GuestRepository.GetBySessionId(_userHelper.GetUserId());
        await _unitOfWork.GuestRepository.Delete(guest.Id);
        await Set();
        return guest;
    }
    
    private async Task Set()
    {
        var customers = await _unitOfWork.GuestRepository.GetAll();
        _cache.SetCache(Key,customers);
    }
    
}
using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.UserRepositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories.UserRepositories;

public class GuestRepository(MagazineDbContext context) : IGuestRepository
{
    private readonly MagazineDbContext _context = context;

    public async Task<Guest> Create(Guest entity)
    {
        await _context.Guests.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Guest> Update(Guest entity)
    {
        _context.Guests.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Guest> Delete(Guid id)
    {
        var guest = await _context.Guests.SingleOrDefaultAsync(x => x.Id == id);
        if(guest is null)
            throw new Exception("Guest not found");
        _context.Guests.Remove(guest);
        await _context.SaveChangesAsync();
        return guest;
    }

    public async Task<List<Guest>> GetAll()
    {
        var guests = await _context.Guests.Include(x=> x.Cart).
            Include(x=> x.Orders).AsNoTracking().ToListAsync();
        return guests;
    }

    public async Task<Guest> GetById(Guid id)
    {
        var guest = await _context.Guests.Include(x=> x.Cart).Include(x=> x.Orders)
            .SingleOrDefaultAsync(x => x.Id == id);
        if(guest is null)
            throw new Exception("Guest not found");
        return guest;
    }

    public async Task ClearAll()
    {
        _context.Guests.RemoveRange(_context.Guests);
        await _context.SaveChangesAsync();
    }

    public async Task<Guest> GetBySessionId(Guid sessionId)
    {
        var guest =  await _context.Guests.Include(x=> x.Cart).Include(x=> x.Orders)
            .SingleOrDefaultAsync(x => x.SessionId == sessionId);
        if(guest is null)
            throw new Exception("Guest not found");
        return guest;
    }

    public async Task<List<Guest>> GetByDate(DateTime startDate, DateTime endDate)
    {
        var guests = await _context.Guests.Include(x=> x.Cart).Include(x=> x.Orders)
            .Where(x => x.Created >= startDate && x.Created <= endDate)
            .AsNoTracking().ToListAsync();
        return guests;
    }
}
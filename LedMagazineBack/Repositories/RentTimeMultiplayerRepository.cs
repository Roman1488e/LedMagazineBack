using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories;

public class RentTimeMultiplayerRepository(MagazineDbContext context) : IRentTimeMultiplayerRepository
{
    private readonly MagazineDbContext _context = context;

    public async Task<RentTimeMultiplayer> Create(RentTimeMultiplayer entity)
    {
        await _context.RentTimesMultiplayer.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<RentTimeMultiplayer> Update(RentTimeMultiplayer entity)
    {
        _context.RentTimesMultiplayer.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<RentTimeMultiplayer> Delete(Guid id)
    {
        var rentTimeMultiplayer = await _context.RentTimesMultiplayer.SingleOrDefaultAsync(x => x.Id == id);
        if (rentTimeMultiplayer is null)
            throw new Exception("Rent time multiplayer not found");
        _context.RentTimesMultiplayer.Remove(rentTimeMultiplayer);
        await _context.SaveChangesAsync();
        return rentTimeMultiplayer;
    }

    public async Task<List<RentTimeMultiplayer>> GetAll()
    {
        var rentTimes = await _context.RentTimesMultiplayer.AsNoTracking().ToListAsync();
        return rentTimes;
    }

    public async Task<RentTimeMultiplayer> GetById(Guid id)
    {
        var rentTime = await _context.RentTimesMultiplayer.SingleOrDefaultAsync(x => x.Id == id);
        if (rentTime is null)
            throw new Exception("Rent time multiplayer not found");
        return rentTime;
    }

    public async Task<RentTimeMultiplayer> GetByСoefficient(float coefficient)
    {
        var renttimeMult = await _context.RentTimesMultiplayer
            .SingleOrDefaultAsync(x=> Math.Abs(x.MonthsDifferenceMultiplayer - coefficient) <  0.01 || Math.Abs(x.SecondsDifferenceMultiplayer - coefficient) < 0.01);
        if (renttimeMult is null)
            throw new Exception("Rent time multiplayer not found");
        return renttimeMult;
    }

    public async Task<RentTimeMultiplayer> GetByСoefficient(float min, float max)
    {
        var renttimeMult = await _context.RentTimesMultiplayer
            .SingleOrDefaultAsync(x=> x.MonthsDifferenceMultiplayer >= min && x.MonthsDifferenceMultiplayer <= max ||  x.SecondsDifferenceMultiplayer >= min && x.SecondsDifferenceMultiplayer <= max);
        if (renttimeMult is null)
            throw new Exception("Rent time multiplayer not found");
        return renttimeMult; 
    }
}
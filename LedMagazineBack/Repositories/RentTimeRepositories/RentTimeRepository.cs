using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.RentTimeRepositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories.RentTimeRepositories;

public class RentTimeRepository(MagazineDbContext context) : IRentTimeRepository
{
    private readonly MagazineDbContext _context = context;
    public async Task<RentTime> Create(RentTime entity)
    {
        await _context.RentTimes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<RentTime> Update(RentTime entity)
    {
        _context.RentTimes.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<RentTime> Delete(Guid id)
    {
        var  rentTime = await _context.RentTimes.SingleOrDefaultAsync(x=> x.Id == id);
        if (rentTime is null)
            throw new Exception("RentTime not found");
        _context.RentTimes.Remove(rentTime);
        await _context.SaveChangesAsync();
        return rentTime;
    }

    public async Task<List<RentTime>> GetAll()
    {
        var rentTimes = await _context.RentTimes.AsNoTracking().ToListAsync();
        return rentTimes;
    }

    public async Task<RentTime> GetById(Guid id)
    {
        var rentTime = await _context.RentTimes.SingleOrDefaultAsync(x => x.Id == id);
        if (rentTime is null)
            throw new Exception("RentTime not found");
        return rentTime;
    }

    public async Task<RentTime> GetByCartItemId(Guid cartItemId)
    {
        var  rentTime = await _context.RentTimes.SingleOrDefaultAsync(x => x.Id == cartItemId);
        if (rentTime is null)
            throw new Exception("RentTime not found");
        return rentTime;
    }

    public async Task<RentTime> GetByOrderItemId(Guid orderItemId)
    {
        var rentTime = await _context.RentTimes.SingleOrDefaultAsync(x => x.Id == orderItemId);
        if (rentTime is null)
            throw new Exception("RentTime not found");
        return rentTime;
    }

    public async Task<List<RentTime>> GetByMonths(int minmonth, int maxmonth)
    {
        var rentTimes = await _context.RentTimes.AsNoTracking()
            .Where(x=> x.RentMonths >= minmonth && x.RentMonths <= maxmonth).ToListAsync();
        return rentTimes;
    }

    public async Task<List<RentTime>> GetBySeconds(int minsec, int maxsec)
    {
        var rentTimes = await _context.RentTimes.AsNoTracking()
            .Where(x=> x.RentSeconds >= minsec && x.RentSeconds <= maxsec).ToListAsync();
        return rentTimes;
    }
}
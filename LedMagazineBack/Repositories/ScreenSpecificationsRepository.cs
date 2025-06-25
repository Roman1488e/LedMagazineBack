using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories;

public class ScreenSpecificationsRepository(MagazineDbContext context) : IScreenSpecificationRepository
{
    private readonly MagazineDbContext _context = context;

    public async Task<ScreenSpecifications> Create(ScreenSpecifications entity)
    {
        await _context.ScreenSpecifications.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<ScreenSpecifications> Update(ScreenSpecifications entity)
    {
        _context.ScreenSpecifications.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<ScreenSpecifications> Delete(Guid id)
    {
        var entity = await _context.ScreenSpecifications.SingleOrDefaultAsync(x => x.Id == id);
        if(entity is null)
            throw new Exception("Screen Specification not found");
        _context.ScreenSpecifications.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<ScreenSpecifications>> GetAll()
    {
        var screenSpecifications = await _context.ScreenSpecifications.AsNoTracking().ToListAsync();
        return screenSpecifications;
    }

    public async Task<ScreenSpecifications> GetByProductId(Guid productId)
    {
        var screenSpecifications = await _context.ScreenSpecifications
            .SingleOrDefaultAsync(x => x.ProductId == productId);
        if (screenSpecifications is null)
            throw new Exception("Screen Specification not found");
        return screenSpecifications;
    }

    public async Task<List<ScreenSpecifications>> GetByScreenSize(string screenSize)
    {
        var screenSpecifications = await _context.ScreenSpecifications
            .AsNoTracking().Where(x => x.ScreenSize.Equals(screenSize, StringComparison.CurrentCultureIgnoreCase)).ToListAsync();
        return screenSpecifications;
    }

    public async Task<List<ScreenSpecifications>> GetByResolution(string resolution)
    {
        var screenSpecifications = await _context.ScreenSpecifications
            .AsNoTracking().Where(x => x.ScreenResolution.Equals(resolution, StringComparison.CurrentCultureIgnoreCase)).ToListAsync();
        return screenSpecifications; 
    }

    public async Task<List<ScreenSpecifications>> GetByScreenType(string screenType)
    {
        var screenSpecifications = await _context.ScreenSpecifications
            .AsNoTracking().Where(x => x.ScreenType.Equals(screenType, StringComparison.CurrentCultureIgnoreCase)).ToListAsync();
        return screenSpecifications;
    }
}
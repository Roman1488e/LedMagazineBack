using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.ProductRepositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories.ProductRepositories;

public class LocationRepository(MagazineDbContext context) : ILocationRepository
{
    private readonly MagazineDbContext _context = context;
    public async Task<Location> Create(Location entity)
    {
        await _context.Locations.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Location> Update(Location entity)
    {
        _context.Locations.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Location> Delete(Guid id)
    {
        var location = await _context.Locations.SingleOrDefaultAsync(x=> x.Id == id);
        if (location == null)
            throw new Exception("Location not found");
        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
        return location;
    }

    public async Task<List<Location>> GetAll()
    {
        var locations = await _context.Locations.AsNoTracking().ToListAsync();
        return locations;
    }

    public async Task<Location> GetById(Guid id)
    {
        var location = await _context.Locations.SingleOrDefaultAsync(x => x.Id == id);
        if (location is null)
            throw new Exception("Location not found");
        return location;
    }

    public async Task<Location> GetByProductId(Guid productId)
    {
        var location = await _context.Locations.SingleOrDefaultAsync(x => x.ProductId == productId);
        if (location is null)
            throw new Exception("Location not found");
        return location;
    }

    public async Task<List<Location>> GetByDistrict(string district)
    {
        var locations = await _context.Locations.AsNoTracking().Where(x=> x.District == district).ToListAsync();
        return locations;
    }
}
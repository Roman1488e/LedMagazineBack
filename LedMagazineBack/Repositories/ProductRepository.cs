using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories;

public class ProductRepository(MagazineDbContext context) : IProductRepository
{
    private  readonly MagazineDbContext _context = context;

    public async Task<Product> Create(Product entity)
    {
        await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Product> Update(Product entity)
    {
        _context.Products.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Product> Delete(Guid id)
    {
        var product = await _context.Products.SingleOrDefaultAsync(x=> x.Id == id);
        if(product is null)
            throw new Exception("Product not found");
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<List<Product>> GetAll()
    {
        var products = await _context.Products.AsNoTracking().Include(x=> x.Location).Include(x=> x.RentTimeMultiplayer)
            .Include(x=> x.ScreenSpecifications).ToListAsync();
        return products;
    }

    public async Task<Product> GetById(Guid id)
    {
        var product = await _context.Products.Include(x=> x.Location).Include(X=>X.RentTimeMultiplayer)
            .Include(x=> x.ScreenSpecifications).SingleOrDefaultAsync(x => x.Id == id);
        if(product is null)
            throw new Exception("Product not found");
        return product;
    }

    public async Task<List<Product>> GetActive()
    {
        var products = await _context.Products.AsNoTracking().Include(x=> x.Location).Include(x=> x.RentTimeMultiplayer)
            .Include(x=> x.ScreenSpecifications).ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetFiltered(
        string? districts,
        string? screenSizes,
        decimal minPrice,
        decimal maxPrice,
        string? screenResolutions,
        int page = 1,
        int pageSize = 10)
    {
        var query = _context.Products
            .AsNoTracking()
            .Include(x => x.Location)
            .Include(x => x.ScreenSpecifications)
            .Include(x=> x.RentTimeMultiplayer)
            .AsQueryable();

        if (!string.IsNullOrEmpty(districts))
        {
            var districtList = districts.Split(',').Select(d => d.Trim()).ToList();
            query = query.Where(x => districtList.Contains(x.Location.District));
        }

        if (!string.IsNullOrEmpty(screenSizes))
        {
            var sizeList = screenSizes.Split(',').Select(s => s.Trim()).ToList();
            query = query.Where(x => sizeList.Contains(x.ScreenSpecifications.ScreenSize));
        }

        if (!string.IsNullOrEmpty(screenResolutions))
        {
            var resolutionList = screenResolutions.Split(',').Select(r => r.Trim()).ToList();
            query = query.Where(x => resolutionList.Contains(x.ScreenSpecifications.ScreenResolution));
        }

        if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
        {
            query = query.Where(x => x.BasePrice >= minPrice && x.BasePrice <= maxPrice);
        }
        
        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync();
    }

}
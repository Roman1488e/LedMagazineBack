using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.ProductRepositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories.ProductRepositories;

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
        var products = await _context.Products.AsNoTracking().Include(x=> x.Location)
            .Include(x=>x.ScreenSpecifications).ToListAsync();
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

    public async Task<List<Product>> GetAll(
        string? districts,
        string? screenSizes,
        decimal minPrice,
        decimal maxPrice,
        string? screenResolutions,
        bool? isActive,
        int page = 1,
        int pageSize = 10)
    {
        var query = _context.Products
            .AsNoTracking()
            .Include(x => x.Location)
            .Include(x => x.ScreenSpecifications)
            .Include(x => x.RentTimeMultiplayer)
            .AsQueryable();
        
        if (isActive.HasValue)
        {
            query = query.Where(x => x.IsActive == isActive.Value);
        }

        if (!string.IsNullOrEmpty(districts))
        {
            var districtList = districts.Split(',').Select(d => d.Trim().ToLower()).ToList();
            query = query.Where(x => districtList.Contains(x.Location.District.ToLower()));
        }

        if (!string.IsNullOrEmpty(screenSizes))
        {
            var sizeList = screenSizes.Split(',').Select(s => s.Trim().ToLower()).ToList();
            query = query.Where(x => x.ScreenSpecifications != null && sizeList.Contains(x.ScreenSpecifications.ScreenSize.ToLower()));
        }

        if (!string.IsNullOrEmpty(screenResolutions))
        {
            var resolutionList = screenResolutions.Split(',').Select(r => r.Trim().ToLower()).ToList();
            query = query.Where(x => x.ScreenSpecifications != null && resolutionList.Contains(x.ScreenSpecifications.ScreenResolution.ToLower()));
        }

        if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
        {
            query = query.Where(x => x.BasePrice >= minPrice && x.BasePrice <= maxPrice);
        }

        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync();
    }



}
using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories;

public class OrderRepository(MagazineDbContext context) : IOrderRepository
{
    private readonly MagazineDbContext  _context = context;

    public async Task<Order> Create(Order entity)
    {
        await _context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Order> Update(Order entity)
    {
        await _context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Order> Delete(Guid id)
    {
        var entity = await _context.Orders.SingleOrDefaultAsync(x=>x.Id == id);
        if (entity == null)
            throw new Exception($"Order not found");
        _context.Orders.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Order> GetById(Guid id)
    {
        var entity = await _context.Orders
            .Include(x=>x.Items)
            .SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            throw new Exception($"Order not found");
        return entity;
    }

    public async Task<List<Order>> GetActive()
    {
        var orders = await _context.Orders.AsNoTracking().Include(x => x.Items)
            .Where(x=> x.IsActive).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetAll()
    {
        var orders = await _context.Orders.AsNoTracking().Include(x => x.Items)
            .ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetPrimary()
    {
        var orders = await _context.Orders.AsNoTracking().Include(x => x.Items)
            .Where(x=> x.IsPrimary).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetByOrgName(string orgName)
    {
        var orders = await _context.Orders.AsNoTracking().Include(x => x.Items)
            .Where(x=> x.OrganisationName.Contains(orgName)).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetFiltered(DateTime? startDate, DateTime? endDate, decimal minPrice, decimal maxPrice)
    {
        var query = _context.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .AsQueryable();
        
        if (startDate.HasValue)
            query = query.Where(x => x.Created >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(x => x.Created <= endDate.Value);
        
        query = query.Where(x => x.TotalPrice >= minPrice && x.TotalPrice <= maxPrice);

        return await query.ToListAsync();
    }


    

    public async Task<List<Order>> GetByUserId(Guid userId)
    {
        var orders = await _context.Orders.AsNoTracking().Include(x => x.Items)
            .Where(x=> x.CustomerId == userId || x.SessionId == userId).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetNotAccepted()
    {
        var orders = await _context.Orders.AsNoTracking().Include(x => x.Items)
            .Where(x=> !x.IsAccepted).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetAccepted()
    {
        var orders = await _context.Orders.AsNoTracking().Include(x => x.Items)
            .Where(x=> x.IsAccepted).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetByProductName(string productName)
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .Where(order => order.Items.Any(item => item.ProductName == productName))
            .ToListAsync();
    }

}
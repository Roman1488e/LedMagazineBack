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
        var entity = await _context.Orders.Include(x=>x.Products)
            .Include(x=>x.RentTimes)
            .SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            throw new Exception($"Order not found");
        return entity;
    }

    public async Task<List<Order>> GetActive()
    {
        var orders = await _context.Orders.AsNoTracking().Include(x => x.Products)
            .Include(x=> x.RentTimes).Where(x=> x.IsActive).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetAll()
    {
        var orders = await _context.Orders.AsNoTracking().Include(x => x.Products)
            .Include(x=> x.RentTimes).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetByOrgName(string orgName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Order>> GetFiltered(DateTime? startDate, DateTime? endDate, decimal minPrice, decimal maxPrice)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Order>> GetByProductId(Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Order>> GetByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Order>> GetNotAccepted()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Order>> GetAccepted()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Order>> GetByProductName(string productName)
    {
        throw new NotImplementedException();
    }
}
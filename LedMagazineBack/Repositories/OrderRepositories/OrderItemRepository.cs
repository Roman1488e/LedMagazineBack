using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.OrderRepositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories.OrderRepositories;

public class OrderItemRepository(MagazineDbContext context) : IOrderItemRepository
{
    private readonly MagazineDbContext _context = context;

    public async Task<OrderItem> Create(OrderItem entity)
    {
        await _context.OrderItems.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderItem> Update(OrderItem entity)
    {
        _context.OrderItems.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderItem> Delete(Guid id)
    {
        var entity = await _context.OrderItems.SingleOrDefaultAsync(x=> x.Id == id);
        if (entity is null)
            throw new Exception("Entity not found");
        _context.OrderItems.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<OrderItem> GetById(Guid id)
    {
        var entity = await _context.OrderItems.SingleOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            throw new Exception("Entity not found");
        return entity;
    }

    public async Task<List<OrderItem>> GetByOrderId(Guid orderId)
    {
        var entities = await _context.OrderItems
            .Where(x => x.OrderId == orderId).AsNoTracking()
            .ToListAsync();
        return entities;
    }

    public async Task<List<OrderItem>> GetByProductName(string productName)
    {
        var orderItems = await _context.OrderItems
            .AsNoTracking()
            .Where(x => x.ProductName.ToLower().Contains(productName.ToLower()))
            .ToListAsync();
        return orderItems;
    }

    public async Task<List<OrderItem>> GetAll()
    {
        var entities = await _context.OrderItems.AsNoTracking().ToListAsync();
        return entities;
    }
}
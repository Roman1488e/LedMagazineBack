using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.OrderRepositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories.OrderRepositories;

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
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
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
            .ThenInclude(x=> x.RentTime)
            .SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            throw new Exception($"Order not found");
        return entity;
    }

    public async Task<List<Order>> GetAll()
    {
        var orders = await _context.Orders.AsNoTracking()
            .Include(x=> x.Items).ToListAsync();
        return orders;
    }

    public async Task<Order?> GetByOrderNumber(uint orderNumber)
    {
        var order = await _context.Orders
            .Include(x=> x.Items)
            .ThenInclude(x=> x.RentTime)
            .SingleOrDefaultAsync(x => x.OrderNumber == orderNumber);
        return order;
    }


    public async Task<List<Order>> GetAll(
        string? productName = null,
        string? orgName = null,
        bool? isAccepted = null,
        bool? isActive = null,
        bool? isPrimary = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int page = 1,
        int pageSize = 10)
    {
        var query = _context.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(productName))
        {
            query = query.Where(order =>
                order.Items.Any(item => item.ProductName.ToLower().Contains(productName.ToLower())));
        }

        if (!string.IsNullOrWhiteSpace(orgName))
        {
            query = query.Where(order =>
                order.OrganisationName.ToLower().Contains(orgName.ToLower()));
        }

        if (isAccepted.HasValue)
            query = query.Where(order => order.IsAccepted == isAccepted.Value);

        if (isActive.HasValue)
            query = query.Where(order => order.IsActive == isActive.Value);

        if (isPrimary.HasValue)
            query = query.Where(order => order.IsPrimary == isPrimary.Value);

        if (startDate.HasValue)
            query = query.Where(order => order.Created >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(order => order.Created <= endDate.Value);

        if (minPrice.HasValue)
            query = query.Where(order => order.TotalPrice >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(order => order.TotalPrice <= maxPrice.Value);
        
        query = query
            .OrderByDescending(o => o.Created)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return await query.ToListAsync();
    }

    

    public async Task<List<Order>> GetByUserId(Guid userId)
    {
        var orders = await _context.Orders.Include(x => x.Items)
            .Where(x=> x.CustomerId == userId || x.SessionId == userId).ToListAsync();
        return orders;
    }
    

}
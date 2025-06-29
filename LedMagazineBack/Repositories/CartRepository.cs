using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories;

public class CartRepository(MagazineDbContext context) : ICartRepository
{
    private readonly MagazineDbContext _context = context;

    public async Task<Cart> Create(Cart entity)
    {
        await _context.Carts.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Cart> Update(Cart entity)
    {
        _context.Carts.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Cart> Delete(Guid id)
    {
        var cart = await _context.Carts.SingleOrDefaultAsync(x=> x.Id == id);
        if (cart is null)
            throw new Exception("Cart not found");
        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<List<Cart>> GetAll()
    {
        var carts = await _context.Carts.AsNoTracking()
            .Include(x=> x.Items).ToListAsync();
        return carts;
    }

    public async Task<Cart> GetById(Guid id)
    {
        var cart = await _context.Carts
            .Include(x=> x.Items).SingleOrDefaultAsync(x => x.Id == id);
        if (cart is null)
            throw new Exception("Cart not found");
        return cart;
    }

    public async Task<Cart?> GetByCustomerId(Guid customerId)
    {
        var cart = await _context.Carts.AsNoTracking()
            .Include(x=> x.Items).ThenInclude(x=> x.RentTime).SingleOrDefaultAsync(x => x.CustomerId == customerId);
        return cart;
    }

    public async Task<Cart?> GetBySessionId(Guid sessionId)
    {
        var cart = await _context.Carts
            .Include(x=> x.Items).ThenInclude(x=> x.RentTime).SingleOrDefaultAsync(x => x.SessionId == sessionId);
        return cart;
    }

    public async Task<List<Cart>> GetByDate(DateTime startDate, DateTime endDate)
    {
        var carts = await _context.Carts.AsNoTracking()
            .Include(x=> x.Items).Where(x=> x.Created >= startDate && x.Created <= endDate).ToListAsync();
        return carts;
    }
}
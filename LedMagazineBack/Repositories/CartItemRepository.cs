using LedMagazineBack.Context;
using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LedMagazineBack.Repositories;

public class CartItemRepository(MagazineDbContext context) : ICartItemRepository
{
    private readonly MagazineDbContext _context = context;

    public async Task<CartItem> Create(CartItem entity)
    {
        await _context.CartItems.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<CartItem> Update(CartItem entity)
    {
        _context.CartItems.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<CartItem> Delete(Guid id)
    {
        var cartItem = await _context.CartItems.SingleOrDefaultAsync(x=> x.Id == id);
        if (cartItem is null)
            throw new Exception("Cart Item not found");
        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
        return cartItem;
    }

    public async Task<CartItem> GetById(Guid id)
    {
        var cartItem = await _context.CartItems.SingleOrDefaultAsync(x => x.Id == id);
        if (cartItem is null)
            throw new Exception("Cart Item not found");
        return cartItem;
    }

    public async Task<List<CartItem>> GetByCartId(Guid cartId)
    {
        var cartItems = await _context.CartItems.AsNoTracking().Where(x => x.CartId == cartId).ToListAsync();
        return cartItems;
    }

    public async Task<List<CartItem>> GetAll()
    {
        var cartItems = await _context.CartItems.AsNoTracking().ToListAsync();
        return cartItems;
    }
}
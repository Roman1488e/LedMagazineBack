using LedMagazineBack.Entities;
using LedMagazineBack.Models;

namespace LedMagazineBack.Services.Abstract;

public interface ICartItemService
{
    public Task<List<CartItem>> GetAll();
    public Task<CartItem> GetById(Guid id);
    public Task<List<CartItem>> GetByCartId(Guid cartId);
    public Task<CartItem> Create(CreateCartItemModel model);
}
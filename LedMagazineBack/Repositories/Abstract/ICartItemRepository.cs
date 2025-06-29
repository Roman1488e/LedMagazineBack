using LedMagazineBack.Entities;

namespace LedMagazineBack.Repositories.Abstract;

public interface ICartItemRepository : IBaseRepository<CartItem>
{
    public Task<CartItem> GetById(Guid id);
    public Task<List<CartItem>> GetByCartId(Guid cartItemId);
    public Task<List<CartItem>> DeleteRange(List<CartItem> cartItems);
    public Task<List<CartItem>> GetAll();
}
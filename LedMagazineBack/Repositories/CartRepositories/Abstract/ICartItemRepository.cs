using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;

namespace LedMagazineBack.Repositories.CartRepositories.Abstract;

public interface ICartItemRepository : IBaseRepository<CartItem>
{
    public Task<CartItem> GetById(Guid id);
    public Task<List<CartItem>> GetByCartId(Guid cartItemId);
    public Task<List<CartItem>> DeleteRange(List<CartItem> cartItems);
    public Task<List<CartItem>> GetAll();
}
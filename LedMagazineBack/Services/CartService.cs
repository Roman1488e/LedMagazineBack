using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class CartService(IUnitOfWork unitOfWork) : ICartService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<Cart>> GetAll()
    {
        var carts = await _unitOfWork.CartRepository.GetAll();
        return carts;
    }

    public async Task<Cart> GetById(Guid id)
    {
        var cart = await _unitOfWork.CartRepository.GetById(id);
        return cart;
    }

    public async Task<Cart> GetBySessionId(Guid sessionId)
    {
        var cart =  await _unitOfWork.CartRepository.GetBySessionId(sessionId);
        return cart;
    }

    public async Task<Cart> GetByCustomerId()
    {
        throw new NotImplementedException();
    }

    public async Task<Cart> SwitchToClient(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Cart> Delete(Guid id)
    {
        var  cart = await _unitOfWork.CartRepository.Delete(id);
        return cart;
    }

    public async Task<Cart> DeleteBySessionId(Guid sessionId)
    {
        var cart = await _unitOfWork.CartRepository.GetBySessionId(sessionId);
        await _unitOfWork.CartRepository.Delete(cart.Id);
        return cart;
    }

    public async Task<Cart> Create()
    {
        throw new NotImplementedException();
    }

    public async Task<Cart> Create(CreateCartModel cartModel)
    {
        var cart = new Cart()
        {
            SessionId = cartModel.SessionId,
            Created = DateTime.UtcNow,
            TotalPrice = 0
        };
        var result = await _unitOfWork.CartRepository.Create(cart);
        return result;
    }
}
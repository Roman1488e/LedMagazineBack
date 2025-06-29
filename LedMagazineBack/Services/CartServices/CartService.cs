using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.CartServices.Abstract;

namespace LedMagazineBack.Services.CartServices;

public class CartService(IUnitOfWork unitOfWork, UserHelper userHelper) : ICartService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserHelper _userHelper = userHelper;
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

    public async Task<Cart> GetBySessionId()
    {
        var cart =  await _unitOfWork.CartRepository.GetBySessionId(_userHelper.GetUserId());
        if(cart is null)
            throw new Exception("Cart not found");
        return cart;
    }

    public async Task<Cart> GetByCustomerId()
    {
        var cart = await _unitOfWork.CartRepository.GetByCustomerId(_userHelper.GetUserId());
        if(cart is null)
            throw new Exception("Cart not found");
        return cart;
    }
    

    public async Task<Cart> Delete(Guid id)
    {
        var  cart = await _unitOfWork.CartRepository.Delete(id);
        return cart;
    }

    public async Task<Cart> DeleteBySessionId(Guid sessionId)
    {
        var cart = await _unitOfWork.CartRepository.GetBySessionId(sessionId);
        if(cart is null)
            throw new Exception("Cart not found");
        await _unitOfWork.CartRepository.Delete(cart.Id);
        return cart;
    }

    public async Task<Cart> CreateForCustomer()
    {
        var cart = new Cart()
        {
            CustomerId = _userHelper.GetUserId(),
            Created = DateTime.UtcNow,
            TotalPrice = 0
        };
        var result = await _unitOfWork.CartRepository.Create(cart);
        return result;
    }

    public async Task<Cart> CreateForGuest()
    {
        var cart = new Cart()
        {
            SessionId = _userHelper.GetUserId(),
            Created = DateTime.UtcNow,
            TotalPrice = 0
        };
        var result = await _unitOfWork.CartRepository.Create(cart);
        return result;
    }
}
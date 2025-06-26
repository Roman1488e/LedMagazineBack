using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class CartItemService(IUnitOfWork unitOfWork) : ICartItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<CartItem>> GetAll()
    {
        var cartItems = await _unitOfWork.CartItemRepository.GetAll();
        return cartItems;
    }

    public async Task<CartItem> GetById(Guid id)
    {
        var cartItem = await _unitOfWork.CartItemRepository.GetById(id);
        return cartItem;
    }

    public async Task<List<CartItem>> GetByCartId(Guid cartId)
    {
        var cartItems = await _unitOfWork.CartItemRepository.GetByCartId(cartId);
        return cartItems;
    }

    public async Task<CartItem> Create(CreateCartItemModel model)
    {
        var product = await _unitOfWork.ProductRepository.GetById(model.ProductId);
        var rentTime = new RentTime()
        {
            CreatedDate = DateTime.UtcNow,
            RentMonths = model.RentMonths,
            RentSeconds = model.RentSeconds,
            EndOfRentDate = DateTime.UtcNow.AddMonths(model.RentMonths),
        };
        var cartItem = new CartItem()
        {
            ProductName = product.Name,
            ProductId = model.ProductId,
            ImageUrl = product.ImageUrl,
            
        }
    }
}
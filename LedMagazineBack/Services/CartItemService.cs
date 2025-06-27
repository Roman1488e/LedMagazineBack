using LedMagazineBack.Constants;
using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Models;
using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class CartItemService(IUnitOfWork unitOfWork, UserHelper userHelper) : ICartItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserHelper  _userHelper = userHelper;
    private readonly RolesConstants  _rolesConstants = new RolesConstants();

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
        var role = _userHelper.GetRole();
        Cart? cart = new();
        var product = await _unitOfWork.ProductRepository.GetById(model.ProductId);
        if(role == _rolesConstants.Guest)
            cart = await _unitOfWork.CartRepository.GetBySessionId(_userHelper.GetUserId());
        if (role == _rolesConstants.Admin || role == _rolesConstants.Customer)
            cart = await _unitOfWork.CartRepository.GetByCustomerId(_userHelper.GetUserId());
        if(cart is null)
            throw new Exception("Cart not found");
        var cartItem = new CartItem
        {
            ProductId = model.ProductId,
            ProductName = product.Name,
            ImageUrl = product.ImageUrl,
            CartId = cart.Id,
            Price = product.BasePrice * (decimal)product
                .RentTimeMultiplayer.MonthsDifferenceMultiplayer + (decimal)product.RentTimeMultiplayer.SecondsDifferenceMultiplayer * product.BasePrice
        };
        var createdItem = await _unitOfWork.CartItemRepository.Create(cartItem);
        cart.TotalPrice += createdItem.Price;
        await _unitOfWork.CartRepository.Update(cart);
        var rentTime = new RentTime
        {
            CreatedDate = DateTime.UtcNow,
            RentMonths = model.RentMonths,
            RentSeconds = model.RentSeconds,
            EndOfRentDate = DateTime.UtcNow.AddMonths(model.RentMonths),
            CartItemId = createdItem.Id
        };

        await _unitOfWork.RentTimeRepository.Create(rentTime);

        return createdItem;
    }

}
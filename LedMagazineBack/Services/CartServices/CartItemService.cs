using LedMagazineBack.Constants;
using LedMagazineBack.Entities;
using LedMagazineBack.Helpers;
using LedMagazineBack.Models;
using LedMagazineBack.Models.CartModels.CreationModels;
using LedMagazineBack.Repositories.BasicRepositories.Abstract;
using LedMagazineBack.Services.CartServices.Abstract;
using LedMagazineBack.Services.MemoryServices.Abstract;
using LedMagazineBack.Services.PriceServices.Abstract;
using Microsoft.Extensions.Caching.Memory;

namespace LedMagazineBack.Services.CartServices;

public class CartItemService(IUnitOfWork unitOfWork, UserHelper userHelper, IPriceService priceService, IMemoryCacheService memoryCacheService) : ICartItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPriceService _priceService = priceService;
    private readonly IMemoryCacheService _memoryCacheService = memoryCacheService;
    private const string Key = "CartItem";
    private readonly UserHelper  _userHelper = userHelper;
    private readonly RolesConstants  _rolesConstants = new RolesConstants();

    public async Task<List<CartItem>> GetAll()
    {
        var cached = _memoryCacheService.GetCache<List<CartItem>>(Key);
        if (cached is not null)
            return cached;
        await Set();
        var cartItems = await _unitOfWork.CartItemRepository.GetAll();
        return cartItems;
    }

    public async Task<CartItem> GetById(Guid id)
    {
        var cached = _memoryCacheService.GetCache<List<CartItem>>(Key);
        if (cached is not null)
        {
            var item = cached.SingleOrDefault(x => x.Id == id);
            if(item is null)
                throw new Exception("cart item not found");
            return item;
        }
        await Set();
        var cartItem = await _unitOfWork.CartItemRepository.GetById(id);
        return cartItem;
    }

    public async Task<List<CartItem>> GetByCartId(Guid cartId)
    {
        var cached = _memoryCacheService.GetCache<List<CartItem>>(Key);
        if (cached is not null)
        {
            var items = cached.Where(x => x.Id == cartId).ToList();
            return items;
        }
        await Set();
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
            Price = await _priceService.GeneratePrice(model.ProductId, model.RentMonths, model.RentSeconds ),
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
        await Set();
        return createdItem;
    }
    
    private async Task Set()
    {
        var cartItems = await _unitOfWork.CartItemRepository.GetAll();
        _memoryCacheService.SetCache(Key, cartItems);
    } 

}
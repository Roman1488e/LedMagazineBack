using LedMagazineBack.Repositories.Abstract;
using LedMagazineBack.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.CartControllers;

[ApiController]
public class CartController(ICartService cartService) : Controller
{
    private readonly ICartService  _cartService = cartService;

    [HttpGet("api/carts")]
    public async Task<IActionResult> GetAll()
    {
        var carts = await _cartService.GetAll();
        return Ok(carts);
    }

    [HttpGet("api/carts/by-id/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var cart = await _cartService.GetById(id);
            return Ok(cart);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("api/carts/by-session-id/")]
    [Authorize(Roles = "guest")]
    public async Task<IActionResult> GetBySessionId()
    {
        try
        {
            var result = await _cartService.GetBySessionId();
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("api/carts/by-customer-id/")]
    [Authorize(Roles = "admin, customer")]
    public async Task<IActionResult> GetByCustomerId()
    {
        try
        {
            var result = await _cartService.GetByCustomerId();
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("api/carts/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _cartService.Delete(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    
}
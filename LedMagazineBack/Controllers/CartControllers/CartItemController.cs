using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.CartModels.CreationModels;
using LedMagazineBack.Services.CartServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.CartControllers;

[ApiController]
public class CartItemController(ICartItemService service) : Controller
{
    private readonly ICartItemService _service = service;

    [HttpGet("api/cartItems")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAll();
        return Ok(result);
    }

    [HttpGet("api/cartItems/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("api/cartItems")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateCartItemModel cartItem)
    {
        try
        {
            var result = await _service.Create(cartItem);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/cartItems/by-cartId/{id}")]
    [Authorize(Roles = "guest")]
    public async Task<IActionResult> GetByCartId(Guid id)
    {
        try
        {
            var result = await _service.GetByCartId(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
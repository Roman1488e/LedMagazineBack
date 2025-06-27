using LedMagazineBack.Entities;
using LedMagazineBack.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.OrderControllers;

public class OrderItemController(IOrderItemRepository orderItemRepository) : Controller
{
    private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;

    [HttpGet("api/orderitem")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        var orderItems = await _orderItemRepository.GetAll();
        return Ok(orderItems);
    }

    [HttpGet("api/orderitem/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var result = await _orderItemRepository.GetById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("api/orderitem/by-orderid/{id}")]
    [Authorize]
    public async Task<IActionResult> GetByOrderId(Guid id)
    {
        try
        {
            var result = await _orderItemRepository.GetByOrderId(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("api/orderitem.by-productname/{name}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await _orderItemRepository.GetByProductName(name);
        return Ok(result);
    }

    [HttpDelete("api/orderitem/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _orderItemRepository.Delete(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
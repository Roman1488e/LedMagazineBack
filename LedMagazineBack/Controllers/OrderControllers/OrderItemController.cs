using LedMagazineBack.Attributes;
using LedMagazineBack.Models.OrderModels.CreationModels;
using LedMagazineBack.Services.OrderServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.OrderControllers;

[ApiController]
public class OrderItemController(IOrderItemService orderItemService) : Controller
{
    private readonly IOrderItemService _orderItemService = orderItemService;

    [HttpGet("api/orderitem")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        var orderItems = await _orderItemService.GetAll();
        return Ok(orderItems);
    }

    [HttpGet("api/orderitem/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var result = await _orderItemService.GetById(id);
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
            var result = await _orderItemService.GetByOrderId(id);
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
        var result = await _orderItemService.GetByProductName(name);
        return Ok(result);
    }

    [HttpDelete("api/orderitem/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _orderItemService.Delete(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("api/orderitem")]
    [Authorize]
    [Validate]
    public async Task<IActionResult> Create([FromBody]CreateOrderItemModel model)
    {
        try
        {
            var result = await _orderItemService.Create(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
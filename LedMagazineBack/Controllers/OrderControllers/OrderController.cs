using LedMagazineBack.Models.OrderModels.CreationModels;
using LedMagazineBack.Models.OrderModels.FilterModel;
using LedMagazineBack.Models.OrderModels.UpdateModels;
using LedMagazineBack.Services.OrderServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.OrderControllers;

[ApiController]
public class OrderController(IOrderService orderService) : Controller
{
    private readonly IOrderService _orderService = orderService;
    
    [HttpGet("api/orders")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll([FromQuery]FilterOrdersModel  filterOrdersModel)
    {
        var result = await _orderService.GetAll(filterOrdersModel);
        return Ok(result);
    }

    [HttpPost("api/orders/from-cart-users")]
    [Authorize(Roles = "admin, customer")]
    public async Task<IActionResult> CreateOrderFromCart()
    {
        try
        {
            var result = await _orderService.CreateFromCart();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("api/orders/from-cart-guests")]
    [Authorize(Roles = "guest")]
    public async Task<IActionResult> CreateOrderFromCart([FromBody]CreateOrderModel model)
    {
        try
        {
            var result = await _orderService.CreateFromCart(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/orders/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _orderService.GetById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("api/orders/by-order-number/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetByOrderNumber(uint orderNumber)
    {
        var result = await _orderService.GetByOrderNumber(orderNumber);
        return Ok(result);
    }

    [HttpPost("api/orders/for-guests")]
    [Authorize(Roles = "guest")]
    public async Task<IActionResult> Create([FromBody] CreateOrderModel order)
    {
        try
        {
            var result = await _orderService.Create(order);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("api/orders/for-users")]
    [Authorize(Roles = "customer, admin")]
    public async Task<IActionResult> Create()
    {
        try
        {
            var result = await _orderService.Create();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("api/orders/{id}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderModel order)
    {
        try
        {
            var result = await _orderService.Update(id, order);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("api/orders/{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _orderService.Delete(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("api/orders/{id}/accept")]
    [Authorize]
    public async Task<IActionResult> Accept(Guid id)
    {
        try
        {
            var result = await _orderService.Accept(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPut("api/orders/{id}/cancel")]
    [Authorize]
    public async Task<IActionResult> Cancel(Guid id)
    {
        try
        {
            var result = await _orderService.Cancel(id);
            return Ok(result);
        }   
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    
}
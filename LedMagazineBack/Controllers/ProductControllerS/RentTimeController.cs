using LedMagazineBack.Models;
using LedMagazineBack.Models.RentTimeModels.UpdateModels;
using LedMagazineBack.Services.RentTimeServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.ProductControllerS;

[ApiController]
public class RentTimeController(IRentTimeService rentTimeService) : Controller
{
    private readonly IRentTimeService _rentTimeService = rentTimeService;

    [HttpGet("api/rent-times")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _rentTimeService.GetAll();
        return Ok(result);
    }

    [HttpPut("api/rent-times")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRentTimeModel model)
    {
        try
        {
            var result = await _rentTimeService.Update(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/rent-time/by-id/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _rentTimeService.GetById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("api/rent-time/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _rentTimeService.Delete(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("api/rent-time/by-orderItemid/{id}")]
    [Authorize]
    public async Task<IActionResult> GetByOrderItemId(Guid id)
    {
        try
        {
            var result = await _rentTimeService.GetByOrderItemId(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("api/rent-time/by-cartItemId/{id}")]
    [Authorize]
    public async Task<IActionResult> GetByCartItemId(Guid id)
    {
        try
        {
            var result = await _rentTimeService.GetByCartItemId(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }


}
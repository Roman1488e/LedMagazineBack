using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.RentTimeModels.CreationModels;
using LedMagazineBack.Models.RentTimeModels.UpdateModels;
using LedMagazineBack.Services.RentTimeServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.ProductControllerS;

[ApiController]
public class RentTimeMultiplayerController(IRentTimeMultiplayerService rentTimeService) : Controller
{
    private readonly IRentTimeMultiplayerService _rentTimeService = rentTimeService;

    
    [HttpGet("api/rent-time-multiplayers")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _rentTimeService.GetAll();
        return Ok(result);
    }

    [HttpPost("api/rent-time-multiplayers")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] CreateRentTimeMulModel model)
    {
        try
        {
            var result = await _rentTimeService.Create(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("api/rent-time-multiplayers")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRentTimeMultModel model)
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

    [HttpDelete("api/rent-time-multiplayers/{id}")]
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

    [HttpGet("api/rent-time-multiplayers/by-productId/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetByProductId(Guid id)
    {
        try
        {
            var result = await _rentTimeService.GetByProductId(id);
            return Ok(result);
        }
        catch (Exception e)
        {
           return NotFound(e.Message);
        }
    }

    [HttpGet("api/rent-time-multiplayers/by-Id/{id}")]
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
}
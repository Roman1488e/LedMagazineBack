using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.ProductControllerS;

public class ScreenSpecificationsController(IScreenSpecificationsService screenSpecificationsService) : Controller
{
    private readonly IScreenSpecificationsService _screenSpecificationsService = screenSpecificationsService;

    [HttpGet("api/screen-specifications")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _screenSpecificationsService.GetAll();
        return Ok(result);
    }

    [HttpGet("api/screen-specifications/by-id/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _screenSpecificationsService.GetById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("api/screen-specifications/by-product-id/{productId}")]
    [Authorize]
    public async Task<IActionResult> GetByProductId(Guid productId)
    {
        try
        {
            var result = await _screenSpecificationsService.GetByProductId(productId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("api/screen-specifications/create")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] CreateScreenSpecsModel screenSpecifications)
    {
        try
        {
            var result = await _screenSpecificationsService.Create(screenSpecifications);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("api/screen-specifications/{id}/update")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(Guid id, UpdateScreenSpecsModel model)
    {
        try
        {
            var result = await _screenSpecificationsService.Update(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("api/screen-specifications/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _screenSpecificationsService.Delete(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
using LedMagazineBack.Models;
using LedMagazineBack.Models.ProductModels.CreationModels;
using LedMagazineBack.Models.ProductModels.UpdateModels;
using LedMagazineBack.Services.ProductServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.ProductControllerS;

[ApiController]
public class LocationController(ILocationService locationService) : Controller
{
    private readonly ILocationService _locationService = locationService;

    [HttpGet("api/locations")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        var locations = await _locationService.GetAll();
        return Ok(locations);
    }

    [HttpGet("api/locations/by-id/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _locationService.GetById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("api/locations")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] CreateLocationModel model)
    {
        try
        {
            var result = await _locationService.Create(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("api/locations/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _locationService.Delete(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("api/locations/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLocationModel model)
    {
        try
        {
            var result = await _locationService.Update(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/locations/by-productId/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetByProductId(Guid id)
    {
        try
        {
            var locations = await _locationService.GetByProductId(id);
            return Ok(locations);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
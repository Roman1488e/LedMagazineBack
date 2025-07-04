using LedMagazineBack.Services.MemoryServices.Abstract;
using LedMagazineBack.Services.UserServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LedMagazineBack.Controllers.UserControllers;

[ApiController]
public class GuestController(IGuestService guestService) : Controller
{
    private readonly IGuestService _guestService = guestService;

    [HttpPost("api/guests/generate")]
    public async Task<IActionResult> Generate()
    {
        var result = await _guestService.Create();
        return Ok(result);
    }

    [HttpDelete("api/guests/delete-all")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteAll()
    {
        try
        {
            await _guestService.ClearAll();
            return Ok("Deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("api/guests")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _guestService.GetAll();
        return Ok(result);
    }
    
    [HttpGet("api/guests/by-id/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _guestService.GetById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("api/guests/by-sessionId/{sessionId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetBySessionId(Guid sessionId)
    {
        try
        {
            var result = await _guestService.GetBySessionId(sessionId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("api/guests/{sessionId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid sessionId)
    {
        try
        {
            var result = await _guestService.DeleteBySessionId(sessionId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}
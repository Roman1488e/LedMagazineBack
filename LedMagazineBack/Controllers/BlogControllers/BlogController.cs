using LedMagazineBack.Entities;
using LedMagazineBack.Services.BlogServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.BlogControllers;

[ApiController]
public class BlogController(IBlogService blogService) : Controller
{
    private readonly IBlogService _blogService = blogService;

    [HttpGet("api/blogs")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var blogs = await _blogService.GetAll();
        return Ok(blogs);
    }

    [HttpGet("api/blogs/{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var blog = await _blogService.GetById(id);
            return Ok(blog);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("api/blogs")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create()
    {
        try
        {
            var result = await _blogService.Create();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("api/blogs/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var blog = await _blogService.Delete(id);
            return Ok(blog);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}
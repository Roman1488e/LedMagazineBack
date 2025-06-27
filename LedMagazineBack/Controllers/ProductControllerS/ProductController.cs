using LedMagazineBack.Models;
using LedMagazineBack.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.ProductControllerS;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : Controller
{
    private readonly IProductService _productService = productService;

    [HttpGet("api/products")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAll();
        return Ok(result);
    }

    [HttpGet("api/products/{id}")]
    [Authorize]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var result = await _productService.GetById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("api/products")]
    [Authorize(Roles = "admin")]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> Create([FromForm] CreateProductModel model)
    {
        try
        {
            var result = await _productService.Create(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("api/products/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {   
            var result = await _productService.Delete(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("api/products/{id}/general-info")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateGeneralInfo(Guid id, [FromBody] UpdateProductGenInfoModel model)
    {
        try
        {
            var result = await _productService.UpdateGeneralInfo(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("api/products/{id}/image")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateImage(Guid id, [FromForm] UpdateProductImageModel model)
    {
        try
        {
            var result = await _productService.ChangeImage(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("api/products/{id}/video")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateVideo(Guid id, [FromForm] UpdateProductVideoModel model)
    {
        try
        {
            var result = await _productService.ChangeVideo(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("api/products/{id}/price")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdatePrice(Guid id, [FromBody] UpdateProductPriceModel model)
    {
        try
        {
            var result = await _productService.ChangePrice(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("api/products/{id}/isActive")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateGeneralInfo(Guid id)
    {
        try
        {
            var result = await _productService.ChangeIsActive(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("api/products/filter")]
    [Authorize]
    public async Task<IActionResult> GetFiltered(
        [FromQuery] string? districts,
        [FromQuery] string? screenSizes,
        [FromQuery] decimal minPrice,
        [FromQuery] decimal maxPrice,
        [FromQuery] string? screenResolutions,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        try
        {
            var result = await _productService.GetFiltered(
                districts,
                screenSizes,
                minPrice,
                maxPrice,
                screenResolutions,
                page,
                pageSize);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
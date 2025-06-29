using LedMagazineBack.Models;
using LedMagazineBack.Models.BlogModels.CreationModels;
using LedMagazineBack.Models.BlogModels.UpdateModels;
using LedMagazineBack.Services.BlogServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.BlogControllers;

[ApiController]
public class ArticleController(IArticleService articleService) : Controller
{
    private readonly IArticleService _articleService = articleService;

    [HttpGet("api/articles")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var articles = await _articleService.GetAll();
        return Ok(articles);
    }

    [HttpGet("api/articles/by-id/{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var article = await _articleService.GetById(id);
            return Ok(article);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("api/articles/by-title/{title}")]
    [Authorize]
    public async Task<IActionResult> GetByTitle(string title)
    {
        var articles = await _articleService.GetByTitle(title);
        return Ok(articles);
    }

    [HttpGet("api/articles/by-word/{word}")]
    [Authorize]
    public async Task<IActionResult> GetByWord(string word)
    {
        var articles = await _articleService.GetByWord(word);
        return Ok(articles);
    }

    [HttpDelete("api/articles/{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var article = await _articleService.Delete(id);
            return Ok(article);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("api/articles/{id}/general-info")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(Guid id,[FromBody] UpdateArticleModel model)
    {
        try
        {
            var result = await _articleService.Update(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("api/articles/{id}/image")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateImage(Guid id,[FromForm] UpdateArticleImageModel model)
    {
        try
        {
            var result = await _articleService.UpdateImage(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("api/articles/{id}/video")]
    [Authorize(Roles = "admin")]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> UpdateVideo(Guid id,[FromForm] UpdateArticleVideoModel model)
    {
        try
        {
            var result = await _articleService.UpdateVideo(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("api/articles")]
    [Authorize(Roles = "admin")]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> CreateArticle([FromForm] CreateArticleModel model)
    {
        try
        {
            var result = await _articleService.Create(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
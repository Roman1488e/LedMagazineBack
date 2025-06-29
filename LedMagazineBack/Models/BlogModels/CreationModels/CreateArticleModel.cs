namespace LedMagazineBack.Models.BlogModels.CreationModels;

public class CreateArticleModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public IFormFile Image { get; set; }
    public IFormFile? Video { get; set; }
    public Guid BlogId { get; set; }
}
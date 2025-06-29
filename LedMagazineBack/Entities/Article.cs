namespace LedMagazineBack.Entities;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public string ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
    public Guid BlogId { get; set; }
}
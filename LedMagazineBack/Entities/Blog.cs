namespace LedMagazineBack.Entities;

public class Blog
{
    public Guid Id { get; set; }
    public List<Article> Articles { get; set; }
}
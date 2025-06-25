namespace LedMagazineBack.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public string ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string Duration { get; set; }
    public Location Location { get; set; }
    public ScreenSpecifications ScreenSpecifications { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
    public RentTimeMultiplayer RentTimeMultiplayer { get; set; }
    
    
}
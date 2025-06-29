namespace LedMagazineBack.Models.ProductModels.CreationModels;

public class CreateProductModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public IFormFile Image { get; set; }
    public IFormFile? Video { get; set; }
    public string Duration { get; set; }
    public bool IsActive { get; set; }
}
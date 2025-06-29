namespace LedMagazineBack.Models.ProductModels.CreationModels;

public class CreateLocationModel
{
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public string District { get; set; }
    public Guid ProductId { get; set; }
}
namespace LedMagazineBack.Models.ProductModels.CreationModels;

public class CreateLocationModel
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string District { get; set; }
    public Guid ProductId { get; set; }
}
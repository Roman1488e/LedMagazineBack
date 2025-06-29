namespace LedMagazineBack.Models.ProductModels.CreationModels;

public class CreateScreenSpecsModel
{
    public string ScreenSize { get; set; }
    public string ScreenResolution { get; set; }
    public string ScreenType { get; set; }
    public Guid ProductId { get; set; }
}
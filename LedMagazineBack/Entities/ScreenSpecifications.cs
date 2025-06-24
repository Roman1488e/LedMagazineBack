namespace LedMagazineBack.Entities;

public class ScreenSpecifications
{
    public Guid Id { get; set; }
    public string ScreenSize { get; set; }
    public string ScreenResolution { get; set; }
    public string ScreenType { get; set; }
    public Guid ProductId { get; set; }
}
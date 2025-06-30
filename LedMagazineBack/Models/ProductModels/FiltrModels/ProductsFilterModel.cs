namespace LedMagazineBack.Models.ProductModels.FiltrModels;

public class ProductsFilterModel
{
    public string? districts  { get; set; }
    public string? screenSizes {get; set;}
    public decimal minPrice {get; set;}
    public decimal maxPrice {get; set;}
    public string? screenResolutions {get; set;}
    public bool? isActive  { get; set; }
    public int page {get; set;}
    public int pageSize {get; set;}
}
namespace LedMagazineBack.Models.OrderModels.FilterModel;

public class FilterOrdersModel
{
    public string? productName { get; set; }
    public string? orgName {get; set;}
    public bool? isAccepted { get; set; }
    public bool? isActive { get; set; }
    public bool? isPrimary {get; set;}
    public DateTime? startDate { get; set; }
    public DateTime? endDate {get; set;}
    public decimal? minPrice {get; set;}
    public decimal? maxPrice {get; set;}
    public int page { get; set; }
    public int pageSize { get; set; }
}
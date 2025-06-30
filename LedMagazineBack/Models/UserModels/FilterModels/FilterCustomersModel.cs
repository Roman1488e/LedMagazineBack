namespace LedMagazineBack.Models.UserModels.FilterModels;

public class FilterCustomersModel
{
    public string? role {get; set;}
    public string? organisationName {get; set;}
    public string? anyWord { get; set; }
    public bool? isVerified { get; set; }
    public int page {get; set;}
    public int pageSize {get; set;}
}
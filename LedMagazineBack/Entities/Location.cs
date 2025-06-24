namespace LedMagazineBack.Entities;

public class Location
{
    public Guid Id { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public string District { get; set; }
    public Guid ProductId { get; set; }
}
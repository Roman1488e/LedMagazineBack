namespace LedMagazineBack.Entities;

public class Location
{
    public Guid Id { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string District { get; set; }
    public Guid ProductId { get; set; }
}
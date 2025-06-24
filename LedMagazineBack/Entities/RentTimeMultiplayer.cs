namespace LedMagazineBack.Entities;

public class RentTimeMultiplayer
{
    public Guid Id { get; set; }
    public float SecondsDifferenceMultiplayer { get; set; }
    public float MonthsDifferenceMultiplayer { get; set; }
    public Guid ProductId { get; set; }
}
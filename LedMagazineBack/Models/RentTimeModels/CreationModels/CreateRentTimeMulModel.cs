namespace LedMagazineBack.Models.RentTimeModels.CreationModels;

public class CreateRentTimeMulModel
{
    public float SecondsDifferenceMultiplayer { get; set; } = 1;
    public float MonthsDifferenceMultiplayer { get; set; } = 1;
    public Guid ProductId { get; set; }
}
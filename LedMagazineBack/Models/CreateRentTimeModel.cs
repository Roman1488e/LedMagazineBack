namespace LedMagazineBack.Models;

public class CreateRentTimeModel
{
    public byte RentSeconds { get; set; }
    public byte RentMonths { get; set; }
    public Guid? CartItemId { get; set; }
    public Guid? OrderItemId { get; set; }
}
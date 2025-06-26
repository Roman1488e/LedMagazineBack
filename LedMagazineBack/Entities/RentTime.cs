namespace LedMagazineBack.Entities;

public class RentTime
{
    public Guid Id { get; set; }
    public byte RentSeconds { get; set; }
    public byte RentMonths { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime EndOfRentDate { get; set; }
    public Guid? CartItemId { get; set; }
    public Guid? OrderItemId { get; set; }
}
namespace LedMagazineBack.Models.OrderModels.CreationModels;

public class CreateOrderItemModel
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public byte RentSeconds { get; set; }
    public byte RentMonths { get; set; }
}
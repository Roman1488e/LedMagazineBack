namespace LedMagazineBack.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public Guid ProductId { get; set; }
    public RentTime RentTime { get; set; }
    public decimal Price { get; set; }
}
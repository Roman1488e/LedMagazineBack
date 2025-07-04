namespace LedMagazineBack.Entities;

public class CartItem
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public RentTime RentTime { get; set; }
    public decimal Price { get; set; }
}
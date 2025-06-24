namespace LedMagazineBack.Entities;

public class Cart
{
    public Guid Id { get; set; }
    public List<CartItem> Items { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Created { get; set; }
    public Guid? SessionId { get; set; }
    public Guid? UserId { get; set; }
}
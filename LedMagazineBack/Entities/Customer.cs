namespace LedMagazineBack.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string? ContactNumber { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public List<Order> Orders { get; set; }
    public Cart Cart { get; set; }
}
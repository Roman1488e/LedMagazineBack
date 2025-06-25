namespace LedMagazineBack.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
    public bool IsAccepted { get; set; }
    public string OrganisationName { get; set; }
    public int OrderNumber { get; set; }
    public string PhoneNumber { get; set; } 
    public List<RentTime> RentTimes { get; set; }
    public List<Product> Products { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? SessionId { get; set; }
}
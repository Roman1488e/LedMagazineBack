using System.ComponentModel.DataAnnotations.Schema;

namespace LedMagazineBack.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsPrimary { get; set; }
    public string OrganisationName { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint OrderNumber { get; set; }
    public string PhoneNumber { get; set; } 
    public List<OrderItem> Items { get; set; } = [];
    public decimal TotalPrice { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? SessionId { get; set; }
}
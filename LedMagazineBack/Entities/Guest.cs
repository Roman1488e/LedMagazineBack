namespace LedMagazineBack.Entities;

public class Guest
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public DateTime Created { get; set; }
}
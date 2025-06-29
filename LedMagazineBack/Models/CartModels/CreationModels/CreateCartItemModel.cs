namespace LedMagazineBack.Models.CartModels.CreationModels;

public class CreateCartItemModel
{
    public byte RentSeconds { get; set; }
    public byte RentMonths { get; set; }
    public Guid ProductId { get; set; }
}
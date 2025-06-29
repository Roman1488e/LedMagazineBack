namespace LedMagazineBack.Models.UserModels.Auth;

public class RegisterModel
{
    public string Username { get; set; }
    public string ContactNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string OrganisationName { get; set; }
    public string Name { get; set; }
}
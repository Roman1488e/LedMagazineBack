namespace LedMagazineBack.Models.UserModels.UpdateModels;

public class UpdatePasswordModel
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
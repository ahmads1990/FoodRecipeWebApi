namespace FoodRecipeWebApi.ViewModels.Auth.PasswordReset;

public class PasswordResetViewModel
{
    public int UserId { get; set; }
    public string Otp { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

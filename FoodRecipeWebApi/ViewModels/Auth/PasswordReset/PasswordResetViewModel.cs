namespace FoodRecipeWebApi.ViewModels.Auth.PasswordReset;

public class PasswordResetViewModel
{
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

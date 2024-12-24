namespace FoodRecipeWebApi.ViewModels.Auth;

public class AuthViewModel
{
    public int UserID { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsAuthenticated { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresOn { get; set; }
}
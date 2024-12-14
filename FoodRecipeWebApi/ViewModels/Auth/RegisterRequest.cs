namespace FoodRecipeWebApi.ViewModels.Auth;

public record RegisterRequest(
    string Name,
    string Email,
    string Password
    );

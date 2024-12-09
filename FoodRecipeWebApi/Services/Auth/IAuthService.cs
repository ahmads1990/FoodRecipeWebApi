using FoodRecipeWebApi.ViewModels.Auth;

namespace FoodRecipeWebApi.Services.Auth;

public interface IAuthService
{
    Task<AuthViewModel> LoginUser(LoginViewModel loginViewModel);
}

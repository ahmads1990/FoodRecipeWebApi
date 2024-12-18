using FoodRecipeWebApi.ViewModels.Auth;

namespace FoodRecipeWebApi.Services.Auth;

public interface IAuthService
{
    Task<AuthViewModel> LoginUser(LoginViewModel loginViewModel);
    Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default!);
    Task ConfirmEmailAsync(ConfirmEmailRequest request);
    Task ResendConfirmEmailAsync(ViewModels.Auth.ResendConfirmationEmailRequest request);
}

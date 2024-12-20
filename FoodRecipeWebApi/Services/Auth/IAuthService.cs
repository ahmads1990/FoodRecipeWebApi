using FoodRecipeWebApi.ViewModels.Auth;
using FoodRecipeWebApi.ViewModels.Auth.PasswordReset;

namespace FoodRecipeWebApi.Services.Auth;

public interface IAuthService
{
    Task<AuthViewModel> LoginUser(LoginViewModel loginViewModel);
    Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default!);
    Task ConfirmEmailAsync(ConfirmEmailRequest request);
    Task ResendConfirmEmailAsync(ViewModels.Auth.ResendConfirmationEmailRequest request);
    Task<AuthViewModel> RequestResetPassword(RequestPasswordResetViewModel request);
    Task<AuthViewModel> ResetUserPassword(PasswordResetViewModel passwordResetViewModel);
}

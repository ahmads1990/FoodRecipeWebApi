using FoodRecipeWebApi.Services.Auth;
using FoodRecipeWebApi.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace FoodRecipeWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var result = await _authService.LoginUser(loginViewModel);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        //Todo send confirmation mail
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request ,CancellationToken cancellationToken)
    {
        try
        {

            await _authService.RegisterAsync(request, cancellationToken);
            return Ok(new { message = "User registered successfully." });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }
}

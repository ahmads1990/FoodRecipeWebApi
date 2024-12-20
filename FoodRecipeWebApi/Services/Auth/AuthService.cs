using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.Helpers;
using FoodRecipeWebApi.Helpers.Config;
using FoodRecipeWebApi.Models;
using FoodRecipeWebApi.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace FoodRecipeWebApi.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepo;
    private readonly IRepository<UserClaim> _UserClaimRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailSender _emailSender;
    private readonly JwtConfig _jwtConfig;

    public AuthService(IRepository<User> userRepo, IRepository<UserClaim> userClaimRepo, IOptions<JwtConfig> jwtConfig, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
    {
        _userRepo = userRepo;
        _UserClaimRepo = userClaimRepo;
        _httpContextAccessor = httpContextAccessor;
        _emailSender = emailSender;
        _jwtConfig = jwtConfig.Value;
    }

    public async Task<AuthViewModel> LoginUser(LoginViewModel loginViewModel)
    {
        AuthViewModel authDto = new AuthViewModel();

        // find user by email
        var user = _userRepo.GetByCondition(u => u.Email.Equals(loginViewModel.Email)).FirstOrDefault();

        if (user is null)
        {
            authDto.Message = "Email doesn't exist";
            return authDto;
        }

        // Hash the provided password and compare with the user's hashed password
        var passwordHasher = new PasswordHasher<User>();
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, loginViewModel.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            authDto.Message = "Email or password is incorrect";
            return authDto;
        }

        var claims = _UserClaimRepo.GetByCondition(c => c.UserId == user.ID).ToList();

        var jwtToken = TokenHelper.CreateJwtToken(user, claims, _jwtConfig);

        authDto.IsAuthenticated = true;
        authDto.UserID = user.ID;
        authDto.Username = user.Name ?? string.Empty;
        authDto.Email = user.Email ?? string.Empty;
        authDto.Claims = claims.Select(c => c.Type).ToList();
        authDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        authDto.ExpiresOn = jwtToken?.ValidTo ?? DateTime.Now;

        return authDto;
    }

    public async Task RegisterAsync(ViewModels.Auth.RegisterRequest request, CancellationToken cancellationToken)
    {
        var emailIsExist = _userRepo.CheckByConidition(x => x.Email == request.Email);
        if (emailIsExist)
            throw new InvalidOperationException("Email is already registered.");

        var passwordHasher = new PasswordHasher<User>();
        var user = request.Map<User>();
        user.Password = passwordHasher.HashPassword(user, request.Password);
        _userRepo.Add(user);
        await _userRepo.SaveChangesAsync();
        var IsAdded = _userRepo.CheckByConidition(x => x.Email == request.Email);
        if (IsAdded)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            user.ConfirmCode = token;

            _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token)); // URL-safe encoding
            await SendConfimartionEmail(user, encodedToken);
        }

    }


    public async Task ConfirmEmailAsync(ConfirmEmailRequest request)

    {
        if (_userRepo.GetByID(request.UserId) is not { } user)
            throw new InvalidOperationException("Invalid Code");

        if (user.IsConfirmed)
            throw new InvalidOperationException("Email Already Confirmed");
        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
        var storedToken = _userRepo.GetByCondition(x => x.ID == request.UserId && x.ConfirmCode == decodedToken).FirstOrDefault();
        if (storedToken == null)
        {
            throw new InvalidOperationException("Invalid Code");

        }

        if (storedToken.ExpirationDate < DateTime.UtcNow)
        {
            throw new InvalidOperationException("Expired Code");

        }

        user.IsConfirmed = true;
        _userRepo.Update(user);
        await _userRepo.SaveChangesAsync();
    }


    public async Task ResendConfirmEmailAsync(ViewModels.Auth.ResendConfirmationEmailRequest request)
    {
        if (_userRepo.GetByCondition(x => x.Email == request.Email).FirstOrDefault() is not { } user)
            return;

        if (user.IsConfirmed)
            throw new InvalidOperationException("Email Already Confirmed");


        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        user.ConfirmCode = token;
        user.ExpirationDate = DateTime.UtcNow.AddDays(2);
        _userRepo.Update(user);
        await _userRepo.SaveChangesAsync();
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token)); // URL-safe encoding
        await SendConfimartionEmail(user, encodedToken);


        return;

    }
    private async Task SendConfimartionEmail(User user, string code)
    {
        var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
        var emailBody = EmailBodyHelper.GenerateEmailBody("EmailConfirmation", new Dictionary<string, string>
                {
                    {"{{name}}",user.Name},
                    {"{{action_url}}",$"{origin}/Auth/emailConfirmation?userId={user.ID}&code={code}"}
                });
        await _emailSender.SendEmailAsync(user.Email!, "✅Food Recipe: Email Confirmation", emailBody);

        await Task.CompletedTask;

    }
}

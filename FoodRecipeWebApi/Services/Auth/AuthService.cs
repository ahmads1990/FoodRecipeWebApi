using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.Helpers.Config;
using FoodRecipeWebApi.Models;
using FoodRecipeWebApi.ViewModels.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodRecipeWebApi.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepo;
    private readonly IRepository<UserClaim> _UserClaimRepo;
    private readonly JwtConfig _jwtConfig;

    public AuthService(IRepository<User> userRepo, IRepository<UserClaim> userClaimRepo, IOptions<JwtConfig> jwtConfig)
    {
        _userRepo = userRepo;
        _UserClaimRepo = userClaimRepo;
        _jwtConfig = jwtConfig.Value;
    }

    public async Task<AuthViewModel> LoginUser(LoginViewModel loginViewModel)
    {
        AuthViewModel authDto = new AuthViewModel();
        // return if email doesn't exist OR email+password don't match
        var user = _userRepo.GetByCondition(u => u.Email.Equals(loginViewModel.Email)).FirstOrDefault();

        if (user is null || !_userRepo.CheckByConidition(u => u.Password.Equals(loginViewModel.Password)))
        {
            authDto.Message = "Email or Password is incorrect!";
            return authDto;
        }

        var jwtToken = await CreateJwtTokenAsync(user);
        var claims = _UserClaimRepo.GetByCondition(c => c.UserId == user.ID);

        authDto.IsAuthenticated = true;
        authDto.UserID = user.ID;
        authDto.Username = user.Name ?? string.Empty;
        authDto.Email = user.Email ?? string.Empty;
        authDto.Claims = claims.Select(c => c.Type).ToList();
        authDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        authDto.ExpiresOn = jwtToken?.ValidTo ?? DateTime.Now;

        return authDto;
    }

    private async Task<JwtSecurityToken?> CreateJwtTokenAsync(User user)
    {
        if (user is null) return null;
        // get user claims
        var userClaims = _UserClaimRepo.GetByCondition(c => c.UserId == user.ID).ToList();
        // create jwt claims
        var jwtClaims = new[]
        {
                new UserClaim(JwtRegisteredClaimNames.Sub.ToString(), user.Name ?? string.Empty),
                new UserClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new UserClaim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new UserClaim("uid", user.ID.ToString())
        };
        // merge both claims lists and jwtClaims to allClaims
        var allClaims = jwtClaims.Union(userClaims).Select(c => new Claim(c.Type, c.Value));

        // specify the signing key and algorithm
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        // finally create the token
        var jwtSecurityToken = new JwtSecurityToken(
        issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: allClaims,
            expires: DateTime.Now.AddHours(_jwtConfig.DurationInHours),
            signingCredentials: signingCredentials
            );

        return jwtSecurityToken;
    }
}

using FoodRecipeWebApi.Helpers.Config;
using FoodRecipeWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodRecipeWebApi.Helpers;

public static class TokenHelper
{
    public static JwtSecurityToken? CreateJwtToken(User user, List<UserClaim> userClaims, JwtConfig jwtConfig)
    {
        if (user is null) return null;

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
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        // finally create the token
        var jwtSecurityToken = new JwtSecurityToken(
        issuer: jwtConfig.Issuer,
            audience: jwtConfig.Audience,
            claims: allClaims,
            expires: DateTime.Now.AddHours(jwtConfig.DurationInHours),
            signingCredentials: signingCredentials
            );

        return jwtSecurityToken;
    }
}

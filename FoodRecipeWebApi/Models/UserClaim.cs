using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodRecipeWebApi.Models;

public class UserClaim : BaseModel
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;

    public int UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public UserClaim(string type, string value)
    {
        Type = type;
        Value = value;
    }
}

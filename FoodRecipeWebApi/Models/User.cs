namespace FoodRecipeWebApi.Models;

public class User : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IEnumerable<UserClaim> Claims { get; set; } = new List<UserClaim>();
}

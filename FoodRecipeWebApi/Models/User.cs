namespace FoodRecipeWebApi.Models;

public class User : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; } = false;
    public string ConfirmCode {  get; set; } = string.Empty;
    public DateTime ExpirationDate {  get; set; } = DateTime.UtcNow.AddDays(2);
    public IEnumerable<UserClaim> Claims { get; set; } = new List<UserClaim>();
    public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
}

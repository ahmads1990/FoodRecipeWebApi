namespace FoodRecipeWebApi.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}

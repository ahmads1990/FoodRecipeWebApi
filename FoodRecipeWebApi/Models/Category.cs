namespace FoodRecipeWebApi.Models
{
    public class Category : BaseModel 
    {
        public string CategoryName { get; set;}
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}

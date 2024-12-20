namespace FoodRecipeWebApi.ViewModels.RecipeViewModel
{
    public record GetRecipeViewModel(string name, string tag, string description, string imgaeUrl, decimal price, string categoryName);
}

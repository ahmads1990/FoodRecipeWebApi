using FoodRecipeWebApi.DTO.Recipes;

namespace FoodRecipeWebApi.Services.Recipes
{
    public interface IRecipeService
    {
        public void CreateRecipe(CreateRecipeDto dto);
    }
}

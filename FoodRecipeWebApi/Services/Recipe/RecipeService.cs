using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.DTO.Recipes;
using FoodRecipeWebApi.Helpers;
using FoodRecipeWebApi.Models;

namespace FoodRecipeWebApi.Services.Recipes
{
    public class RecipeService(IRepository<Recipe> repository, ImageHelper imageHelper) : IRecipeService
    {
        private readonly IRepository<Recipe> repository = repository;
        private readonly ImageHelper imageHelper = imageHelper;
        public async void CreateRecipe(CreateRecipeDto dto)
        {
            var imagePath = await imageHelper.SaveImageAsync(dto.Image);

            var recipe = dto.Map<Recipe>();
            recipe.ImageUrl = imagePath;
            repository.Add(recipe);
            repository.SaveChanges();
        }
    }
}

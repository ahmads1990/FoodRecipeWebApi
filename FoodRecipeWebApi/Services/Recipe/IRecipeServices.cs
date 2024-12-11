using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.DTO.Recipes;
using FoodRecipeWebApi.Helpers;
using FoodRecipeWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipeWebApi.Services.Recipes
{
    public interface IRecipeServices
    {
        public void CreateRecipe(CreateRecipeDto dto);
    }
    public class RecipeServices(IRepository<Recipe> repository ,ImageHelper imageHelper) : IRecipeServices
    {
        private readonly IRepository<Recipe> repository = repository;
        private readonly ImageHelper imageHelper = imageHelper;
        public async void CreateRecipe(CreateRecipeDto dto)
        {
            var imagePath =  await imageHelper.SaveImageAsync(dto.Image);
            
            var recipe = dto.Map<Recipe>();
            recipe.ImageUrl = imagePath;
            repository.Add(recipe);
            repository.SaveChanges();
        }
    }
}

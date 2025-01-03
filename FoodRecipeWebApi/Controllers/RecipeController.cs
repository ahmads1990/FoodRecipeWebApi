﻿using FoodRecipeWebApi.Helpers;
using FoodRecipeWebApi.Services.Recipes;
using FoodRecipeWebApi.ViewModels;
using FoodRecipeWebApi.ViewModels.RecipeViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController(IRecipeService recipeServices) : ControllerBase
    {
        private readonly IRecipeService recipeServices = recipeServices;
        [HttpGet("GetAllRecipes")]
        public ApiResponseViewModel<IQueryable<GetRecipeViewModel>> GetAll()
        {
            return recipeServices.GetAllRecipes();
        }
        [HttpGet("GetRecipesByPage")]
        public ApiResponseViewModel<IQueryable<GetRecipeViewModel>> GetRecipesByPage(PaginationHelper paginationParams)
        {
            return recipeServices.GetRecipesByPage(paginationParams);
        }
        [HttpGet("GetlRecipeDetails/{id:int}")]
        public ApiResponseViewModel<GetRecipeViewModel> GetRecipeDetails(int id)
        {
            return recipeServices.GetRecipeDetails(id);
        }
        [HttpGet("GetlRecipesByCategory/{id:int}")]
        public ApiResponseViewModel<IQueryable<GetRecipeViewModel>> GetRecipesByCategory(int id)
        {
            return recipeServices.GetRecipesByCategory(id);
        }
        [HttpPost]
        public async Task<ApiResponseViewModel<bool>> CreateRecipe(CreateRecipeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return await recipeServices.CreateRecipe(viewModel);

            }
            else
            {
                return new(400, ModelState);
            }
        }
        [HttpPut("DeleteRecipe/{id:int}")]
        public ApiResponseViewModel<bool> DeleteRecipe(int id)
        {
            return recipeServices.DeleteRecipe(id);
        }
        [HttpPut("UpdateRecipe")]
        public async Task<ApiResponseViewModel<bool>> UpdateRecipe(UpdateRecipeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return await recipeServices.UpdateRecipe(viewModel);
            }
            return new(400, ModelState);
        }

       
    }
}

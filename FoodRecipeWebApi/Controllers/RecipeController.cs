using FoodRecipeWebApi.DTO.Recipes;
using FoodRecipeWebApi.Services.Recipes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController(IRecipeServices recipeServices): ControllerBase
    {
        private readonly IRecipeServices recipeServices = recipeServices;

        [HttpPost]
        public IActionResult CreateRecipe(CreateRecipeDto dto)
        {
            if (ModelState.IsValid)
            {
                recipeServices.CreateRecipe(dto);
                return Ok("Recipe Created");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}

using FoodRecipeWebApi.DTO.Recipes;
using FoodRecipeWebApi.Services.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController(IRecipeService recipeServices) : ControllerBase
    {
        private readonly IRecipeService recipeServices = recipeServices;

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

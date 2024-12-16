using FoodRecipeWebApi.Services.Category;
using FoodRecipeWebApi.ViewModels.CategoryViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public IActionResult Create(AddCategoryViewModel addCategoryViewModel)
    {
        var result = _categoryService.AddCategory(addCategoryViewModel);

        return result ? Ok("Success") : BadRequest("Failed");
    }
}

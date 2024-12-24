using FoodRecipeWebApi.Mappings;
using FoodRecipeWebApi.Services.Category;
using FoodRecipeWebApi.ViewModels;
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

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _categoryService
            .GetCategories()
            .ProjectTo<CategoryViewModel>()
            .ToList();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(AddCategoryViewModel addCategoryViewModel)
    {
        var result = _categoryService.AddCategory(addCategoryViewModel);

        return result ? Ok("Success") : BadRequest("Failed");
    }

    [HttpPatch]
    public IActionResult UpdateName(UpdateCategoryNameViewModel updateCategoryViewModel)
    {
        var result = _categoryService.UpdateCategoryName(updateCategoryViewModel);

        return result ? Ok("Success") : BadRequest("Failed");
    }
    [HttpDelete("{id:int}")]
    public ApiResponseViewModel<bool> DeleteCategory(int id)
    {
        return _categoryService.DeleteCategory(id);
    }
}

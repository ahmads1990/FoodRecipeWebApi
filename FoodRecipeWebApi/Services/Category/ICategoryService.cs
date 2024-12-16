using FoodRecipeWebApi.ViewModels.CategoryViewModels;
using Microsoft.EntityFrameworkCore.Query;

namespace FoodRecipeWebApi.Services.Category;

public interface ICategoryService
{
    public IQueryable<Models.Category> GetCategories();
    public bool AddCategory(AddCategoryViewModel categoryViewModel);
    public bool UpdateCategoryName(UpdateCategoryNameViewModel categoryViewModel);
}

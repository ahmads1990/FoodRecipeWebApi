using FoodRecipeWebApi.ViewModels;
using FoodRecipeWebApi.ViewModels.CategoryViewModels;

namespace FoodRecipeWebApi.Services.Category;

public interface ICategoryService
{
    public IQueryable<Models.Category> GetCategories();
    public bool AddCategory(AddCategoryViewModel categoryViewModel);
    public bool UpdateCategoryName(UpdateCategoryNameViewModel categoryViewModel);
    public ApiResponseViewModel<bool> DeleteCategory(int id);
}

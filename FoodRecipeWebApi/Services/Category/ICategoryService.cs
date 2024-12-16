using FoodRecipeWebApi.ViewModels.CategoryViewModels;

namespace FoodRecipeWebApi.Services.Category;

public interface ICategoryService
{
    public bool AddCategory(AddCategoryViewModel categoryViewModel);
    public bool UpdateCategoryName(UpdateCategoryNameViewModel categoryViewModel);
}

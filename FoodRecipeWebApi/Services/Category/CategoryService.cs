using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.ViewModels.CategoryViewModels;

namespace FoodRecipeWebApi.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Models.Category> _categoryRepository;

    public CategoryService(IRepository<Models.Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public bool AddCategory(AddCategoryViewModel categoryViewModel)
    {
        var category = categoryViewModel.Map<Models.Category>();
        _categoryRepository.Add(category);
        return _categoryRepository.SaveChanges();
    }
}

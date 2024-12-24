using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.Mappings;
using FoodRecipeWebApi.ViewModels;
using FoodRecipeWebApi.ViewModels.CategoryViewModels;

namespace FoodRecipeWebApi.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Models.Category> _categoryRepository;

    public IQueryable<Models.Category> GetCategories()
    {
        return _categoryRepository.GetAll();
    }

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

    public bool UpdateCategoryName(UpdateCategoryNameViewModel categoryViewModel)
    {
        var category = categoryViewModel.Map<Models.Category>();
        _categoryRepository.SaveInclude(category, nameof(Models.Category.Name));
        return _categoryRepository.SaveChanges();
    }
    public ApiResponseViewModel<bool> DeleteCategory(int id)
    {
        var category = _categoryRepository.GetByID(id);
        if (category is null)
        {
            return new(404, "Category Not Found");
        }
        _categoryRepository.SoftDelete(category);
        return new(204, "Category Deleted");

    }

}

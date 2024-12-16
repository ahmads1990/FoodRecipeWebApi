using System.ComponentModel.DataAnnotations;

namespace FoodRecipeWebApi.ViewModels.CategoryViewModels;

public class AddCategoryViewModel
{
    [Length(5, 100)]
    public string Name { get; set; } = string.Empty;
}

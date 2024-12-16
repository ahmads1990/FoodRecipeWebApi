using System.ComponentModel.DataAnnotations;

namespace FoodRecipeWebApi.ViewModels.CategoryViewModels;

public class UpdateCategoryNameViewModel
{
    [Required, Range(1, int.MaxValue)]
    public int Id { get; set; }
    [Required, Length(5, 100)]
    public string Name { get; set; } = string.Empty;
}

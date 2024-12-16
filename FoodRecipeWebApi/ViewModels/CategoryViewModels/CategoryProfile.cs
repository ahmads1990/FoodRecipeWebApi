using AutoMapper;
using FoodRecipeWebApi.Models;

namespace FoodRecipeWebApi.ViewModels.CategoryViewModels;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<AddCategoryViewModel, Category>();
    }
}

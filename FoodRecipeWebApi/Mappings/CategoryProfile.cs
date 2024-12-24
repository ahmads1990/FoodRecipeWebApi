using AutoMapper;
using FoodRecipeWebApi.Models;
using FoodRecipeWebApi.ViewModels.CategoryViewModels;

namespace FoodRecipeWebApi.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryViewModel, Category>().ReverseMap();
        CreateMap<AddCategoryViewModel, Category>();
        CreateMap<UpdateCategoryNameViewModel, Category>();
    }
}

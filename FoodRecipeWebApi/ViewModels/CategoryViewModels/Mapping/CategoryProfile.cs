using AutoMapper;
using FoodRecipeWebApi.Models;

namespace FoodRecipeWebApi.ViewModels.CategoryViewModels.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryViewModel, Category>().ReverseMap();
        CreateMap<AddCategoryViewModel, Category>();
        CreateMap<UpdateCategoryNameViewModel, Category>();
    }
}

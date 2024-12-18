using AutoMapper;
using FoodRecipeWebApi.Models;

namespace FoodRecipeWebApi.ViewModels.RecipeViewModel
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<CreateRecipeViewModel, Recipe>()
                .ForMember(dst => dst.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.Price, options => options.MapFrom(src => src.Price))
                .ForMember(dst => dst.Description, options => options.MapFrom(src => src.Description))
                .ForMember(dst => dst.CategoryId, options => options.MapFrom(src => src.CategroryId))
                .ForMember(dst => dst.Tag, options => options.MapFrom(src => src.Tag));
            CreateMap<Recipe, GetRecipeViewModel>()
                .ForMember(dst => dst.name, options => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.price, options => options.MapFrom(src => src.Price))
                .ForMember(dst => dst.tag, options => options.MapFrom(src => src.Tag))
                .ForMember(dst => dst.categoryName, options => options.MapFrom(src => src.Category.Name));
            CreateMap<UpdateRecipeViewModel, Recipe>()
                .ForMember(dst => dst.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.Price, options => options.MapFrom(src => src.Price))
                .ForMember(dst => dst.Description, options => options.MapFrom(src => src.Description))
                .ForMember(dst => dst.Tag, options => options.MapFrom(src => src.Tag));
            ;
        }
    }
}

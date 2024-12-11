using AutoMapper;
using FoodRecipeWebApi.Models;

namespace FoodRecipeWebApi.DTO.Recipes
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<CreateRecipeDto, Recipe>()
                .ForMember(dst => dst.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.Price, options => options.MapFrom(src => src.Price))
                .ForMember(dst => dst.CategroryId, options => options.MapFrom(src => src.CategroryId))
                .ForMember(dst => dst.Tag, options => options.MapFrom(src => src.Tag));

        }
    }
}

using AutoMapper;
using FoodRecipeWebApi.Models;
using FoodRecipeWebApi.ViewModels.FavouritesViewModels;

namespace FoodRecipeWebApi.Mappings
{
    public class FavouriteProfile:Profile
    {
        public FavouriteProfile()
        {
            CreateMap<Favourite, GetUserFavouritesViewModel>()
                .ForMember(dest => dest.RecipeName, opt => opt.MapFrom(src => src.Recipe.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Recipe.ImageUrl))
                .ForMember(dest => dest.RecipeDescription, opt => opt.MapFrom(src => src.Recipe.Description));
            CreateMap<AddToFavouritesViewModel, Favourite>();
        }   
    }
}

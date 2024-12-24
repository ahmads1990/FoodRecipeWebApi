using AutoMapper;
using FoodRecipeWebApi.Models;
using FoodRecipeWebApi.ViewModels.Auth;

namespace FoodRecipeWebApi.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterRequest, User>();
    }
}

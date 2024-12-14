using AutoMapper;
using FoodRecipeWebApi.Models;

namespace FoodRecipeWebApi.ViewModels.Auth;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterRequest, User>();
    }
}

using FoodRecipeWebApi.ViewModels;

namespace FoodRecipeWebApi.Services.Favourites;

public interface IFavouriteService
{
public ApiResponseViewModel<bool> DeleteFromFavourites(int id);
}

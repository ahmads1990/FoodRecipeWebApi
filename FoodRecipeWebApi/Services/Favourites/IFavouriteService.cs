using FoodRecipeWebApi.ViewModels;
using FoodRecipeWebApi.ViewModels.FavouritesViewModels;

namespace FoodRecipeWebApi.Services.Favourites;

public interface IFavouriteService
{
    public ApiResponseViewModel<bool> DeleteFromFavourites(int id);
    public ApiResponseViewModel<IQueryable<GetUserFavouritesViewModel>> GetUserFavourites(int userId);
    public ApiResponseViewModel<IQueryable<GetUserFavouritesViewModel>> GetUserFavouritesByPage(GetUserFavouritesByPageViewModel viewModel);
    public ApiResponseViewModel<bool> AddToFavourite(AddToFavouritesViewModel viewModel);
}

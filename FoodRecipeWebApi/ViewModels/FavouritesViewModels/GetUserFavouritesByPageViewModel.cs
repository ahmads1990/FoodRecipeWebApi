using FoodRecipeWebApi.Helpers;

namespace FoodRecipeWebApi.ViewModels.FavouritesViewModels
{
    public class GetUserFavouritesByPageViewModel
    {
        public int userId { get; set; }
        public PaginationHelper paginationHelper { get; set; }
    }
}

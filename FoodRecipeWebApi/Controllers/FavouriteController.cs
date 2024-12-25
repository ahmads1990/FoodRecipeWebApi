using FoodRecipeWebApi.Services.Favourites;
using FoodRecipeWebApi.ViewModels;
using FoodRecipeWebApi.ViewModels.FavouritesViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly IFavouriteService favouriteService;
        public FavouriteController(IFavouriteService favouriteService)
        {
            this.favouriteService = favouriteService;
        }
        [HttpGet("GetUserFavourites/{userId:int}")]
        public ApiResponseViewModel<IQueryable<GetUserFavouritesViewModel>> GetUserFavourites(int userId)
        {
            return favouriteService.GetUserFavourites(userId);
        }
        [HttpGet("GetUserFavouritesByPage")]
        public ApiResponseViewModel<IQueryable<GetUserFavouritesViewModel>> GetUserFavouritesByPage(GetUserFavouritesByPageViewModel viewModel)
        {
            return favouriteService.GetUserFavouritesByPage(viewModel);
        }
        [HttpPost("AddToFavourite")]
        public ApiResponseViewModel<bool> AddToFavourite(AddToFavouritesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return favouriteService.AddToFavourite(viewModel);
            }
            return new ApiResponseViewModel<bool>(400, ModelState);
        }
        [HttpDelete("DeleteFromFavourites/{id:int}")]
        public ApiResponseViewModel<bool> DeleteFromFavourites(int id)
        {
            return favouriteService.DeleteFromFavourites(id);
        }   
    }
}

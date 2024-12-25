using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.Mappings;
using FoodRecipeWebApi.Models;
using FoodRecipeWebApi.Services.Recipes;
using FoodRecipeWebApi.ViewModels;
using FoodRecipeWebApi.ViewModels.FavouritesViewModels;

namespace FoodRecipeWebApi.Services.Favourites
{
    public class FavouriteService(IRepository<Favourite> repository,IRecipeService recipeService) : IFavouriteService
    {
        private readonly IRepository<Favourite> _repository=repository;
        private readonly IRecipeService recipeService = recipeService;

        public ApiResponseViewModel<bool> AddToFavourite(AddToFavouritesViewModel viewModel)
        {
            var result = recipeService.GetRecipeDetails(viewModel.RecipeId);
            if (result.StatusCode==404)
            {
                return new(result.StatusCode, result.Message);
            }
            var favourite = viewModel.Map<Favourite>();
            _repository.Add(favourite);
            return new(201, "Recipe Added To Favourites");
        }
        public ApiResponseViewModel<IQueryable<GetUserFavouritesViewModel>> GetUserFavouritesByPage(GetUserFavouritesByPageViewModel viewModel)
        {
            var userFavourites = _repository.GetByPage(viewModel.paginationHelper).Where(e=>e.UserID==viewModel.userId);
            var data = userFavourites.ProjectTo<GetUserFavouritesViewModel>();
            return new(200, data, "Process Success");
        }

        public ApiResponseViewModel<bool> DeleteFromFavourites(int id)
        {
            var favourite = _repository.GetByID(id);
            if (favourite is null)
            {
                return new(404, "Recipe not in Favouries");
            }
            //_repository.SoftDelete(favourite);
            _repository.Delete(favourite);
            return new(204, "Recipe Removed From Favourites");
        }

        public ApiResponseViewModel<IQueryable<GetUserFavouritesViewModel>> GetUserFavourites(int userId)
        {
            var userFavourites = _repository.GetByCondition(f => f.UserID == userId);
            var data = userFavourites.ProjectTo<GetUserFavouritesViewModel>();
            return new(200, data, "Process Success");
        }
    }
}
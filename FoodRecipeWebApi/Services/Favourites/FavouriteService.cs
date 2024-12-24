using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.Models;
using FoodRecipeWebApi.ViewModels;
namespace FoodRecipeWebApi.Services.Favourites;

public class FavouriteService(IRepository<Favourite> repository) : IFavouriteService
    {
        private readonly IRepository<Favourite> _repository;

        public ApiResponseViewModel<bool> DeleteFromFavourites(int id)
        {
            var recipe =  _repository.GetByID(id);
            if (recipe is null)
            {
                return  new(404, "Recipe Not Found");
            }
            _repository.SoftDelete(recipe);
            return new(204, "Recipe Deleted");
        }
    }
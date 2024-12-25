using FoodRecipeWebApi.Helpers;
using FoodRecipeWebApi.ViewModels;
using FoodRecipeWebApi.ViewModels.RecipeViewModel;

namespace FoodRecipeWebApi.Services.Recipes
{
    public interface IRecipeService
    {
        public Task<ApiResponseViewModel<bool>> CreateRecipe(CreateRecipeViewModel viewModel);

        public ApiResponseViewModel<IQueryable<GetRecipeViewModel>> GetAllRecipes();
        public ApiResponseViewModel<IQueryable<GetRecipeViewModel>> GetRecipesByPage(PaginationHelper paginationParams);
        public ApiResponseViewModel<GetRecipeViewModel> GetRecipeDetails(int id);
        public ApiResponseViewModel<bool> DeleteRecipe(int id);
        public ApiResponseViewModel<IQueryable<GetRecipeViewModel>> GetRecipesByCategory(int categoryId);
        public Task<ApiResponseViewModel<bool>> UpdateRecipe(UpdateRecipeViewModel viewModel);
        public ApiResponseViewModel<bool> DeleteRecipeFromFavourites(int id);
    }
}

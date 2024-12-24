using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.Helpers;
using FoodRecipeWebApi.Mappings;
using FoodRecipeWebApi.Models;
using FoodRecipeWebApi.Services.Category;
using FoodRecipeWebApi.Services.Favourites;
using FoodRecipeWebApi.ViewModels;
using FoodRecipeWebApi.ViewModels.RecipeViewModel;

namespace FoodRecipeWebApi.Services.Recipes
{
    public class RecipeService(IRepository<Recipe> repository, ImageHelper imageHelper, ICategoryService categoryService, IFavouriteService favService) : IRecipeService
    {
        private readonly IRepository<Recipe> repository = repository;
        private readonly ImageHelper imageHelper = imageHelper;
        private readonly ICategoryService categoryService = categoryService;
        private readonly IFavouriteService favouriteService = favService;
        public ApiResponseViewModel<IQueryable<GetRecipeViewModel>> GetAllRecipes()
        {
            var recipes = repository.GetAllWithoutDeleted();
            var data = recipes.ProjectTo<GetRecipeViewModel>();
            return new(200, data, "Process Success");
        }
        public ApiResponseViewModel<GetRecipeViewModel> GetRecipeDetails(int id)
        {
            var recipe = repository.GetByID(id);
            if (recipe is null)
            {
                return new(404, "Recipe Not Found");
            }
            var data = recipe.Map<GetRecipeViewModel>();
            return new(200, data, "Success");

        }
        public ApiResponseViewModel<IQueryable<GetRecipeViewModel>> GetRecipesByCategory(int categoryId)
        {
            var recipesByCategory = repository.GetByCondition(r => r.CategoryId == categoryId);
            var data = recipesByCategory.ProjectTo<GetRecipeViewModel>();
            return new(200, data, "Process Success");
        }

        public async Task<ApiResponseViewModel<bool>> CreateRecipe(CreateRecipeViewModel viewModel)
        {
            var imagePath = await imageHelper.SaveImageAsync(viewModel.Image);

            var recipe = viewModel.Map<Recipe>();
            recipe.ImageUrl = imagePath;
            repository.Add(recipe);
            await repository.SaveChangesAsync();
            return new(201, "Created");
        }
        public async Task<ApiResponseViewModel<bool>> UpdateRecipe(UpdateRecipeViewModel viewModel)
        {
            if (!repository.CheckExistsByID(viewModel.Id))
            {
                return new ApiResponseViewModel<bool>(404, "Recipe Not Found");
            }
            var recipe = viewModel.Map<Recipe>();
            var imageurl = await imageHelper.SaveImageAsync(viewModel.Image);
            recipe.ImageUrl = imageurl;
            repository.SaveInclude(recipe, nameof(recipe.Name), nameof(recipe.ImageUrl),
                nameof(recipe.Description), nameof(recipe.Tag), nameof(recipe.Price));
            await repository.SaveChangesAsync();
            return new ApiResponseViewModel<bool>(204, "Recipe Updated");
        }

        public ApiResponseViewModel<bool> DeleteRecipe(int id)
        {
            var recipe = repository.GetByID(id);
            if (recipe is null)
            {
                return new(404, "Recipe Not Found");
            }
            repository.SoftDelete(recipe);
            return new(204, "Recipe Deleted");
        }
        public ApiResponseViewModel<bool> DeleteRecipeFromFavourites(int id)
        {
            return favouriteService.DeleteFromFavourites(id);
        }
    }
}

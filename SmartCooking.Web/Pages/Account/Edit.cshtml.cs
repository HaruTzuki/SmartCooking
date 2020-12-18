using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Recipes;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages.Account
{
    public class EditModel : UserPageModel
    {
		private readonly IUserFavoriteRecipeRepository userFavoriteRecipeRepository;
		private readonly IRecipeRepository recipeRepository;
		private readonly IRecipeImageRepository recipeImageRepository;

		public IEnumerable<UserFavoriteRecipe> UserFavoriteRecipes { get; set; }
		public IEnumerable<RecipeHeader> RecipeHeaders { get; set; }
		public IEnumerable<RecipeImage> RecipeImages { get; set; }

		public EditModel(IUserFavoriteRecipeRepository userFavoriteRecipeRepository, IRecipeRepository recipeRepository, IRecipeImageRepository recipeImageRepository, IUserRepository userRepository) : base(userRepository)
		{
			this.userFavoriteRecipeRepository = userFavoriteRecipeRepository;
			this.recipeRepository = recipeRepository;
			this.recipeImageRepository = recipeImageRepository;
		}

		public async Task<IActionResult> OnGetAsync()
        {
			if (!IsUserLogged())
			{
				RedirectToPage(Url.Content($"~/Home/Index"));
			}

			this.UserFavoriteRecipes = await userFavoriteRecipeRepository.GetUserFavoriteRecipes(CurrentUser.Id);
			this.RecipeHeaders = await recipeRepository.GetRecipeHeaders();
			this.RecipeImages = await recipeImageRepository.GetRecipeImages();

			return Page();
        }
    }
}

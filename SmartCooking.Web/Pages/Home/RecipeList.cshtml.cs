using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartCooking.Common.Enumeration;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Recipes;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages.Home
{
	public class RecipeListModel : UserPageModel
	{
		private readonly IRecipeRepository recipeRepository;
		private readonly IRecipeImageRepository recipeImageRepository;
		private readonly IUserFavoriteRecipeRepository userFavoriteRecipeRepository;

		[BindProperty] public IEnumerable<RecipeHeader> RecipeHeaders { get; set; }
		[BindProperty] public IEnumerable<RecipeImage> RecipeImages { get; set; }
		[BindProperty] public IEnumerable<UserFavoriteRecipe> UserFavoriteRecipes { get; set; }

		[BindProperty(SupportsGet = true)] public UserRecipeOrderBy UserRecipeOrderBy { get; set; }

		public RecipeListModel(IRecipeRepository recipeRepository, IRecipeImageRepository recipeImageRepository, IUserFavoriteRecipeRepository userFavoriteRecipeRepository, IUserRepository
			userRepository) : base(userRepository)
		{
			this.recipeRepository = recipeRepository;
			this.recipeImageRepository = recipeImageRepository;
			this.userFavoriteRecipeRepository = userFavoriteRecipeRepository;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			GetSessionValues();
			RecipeHeaders = (await recipeRepository.GetRecipeHeaders()).Where(x => x.RecipeType == Common.Enumeration.RecipeType.Done);
			RecipeImages = (await recipeImageRepository.GetRecipeImages()).Where(x => x.ProfileImage);

			if (IsUserLogged() && CurrentUser != null)
			{
				UserFavoriteRecipes = await userFavoriteRecipeRepository.GetUserFavoriteRecipes(CurrentUser.Id);
			}

			if(UserRecipeOrderBy == UserRecipeOrderBy.Ascending)
			{
				RecipeHeaders = RecipeHeaders.OrderBy(x => x.Name);
			}
			else if(UserRecipeOrderBy == UserRecipeOrderBy.Descending)
			{
				RecipeHeaders = RecipeHeaders.OrderByDescending(x => x.Name);
			}
			else if(UserRecipeOrderBy == UserRecipeOrderBy.Favorite)
			{
				var favoriteRecipes = new List<RecipeHeader>();
				foreach(var recipeHeader in RecipeHeaders)
				{
					if(UserFavoriteRecipes.Any(x=>x.RecipeHeaderId == recipeHeader.Id))
					{
						favoriteRecipes.Add(recipeHeader);
					}
				}

				RecipeHeaders = favoriteRecipes.AsEnumerable().OrderBy(x=>x.Name);
			}

			return Page();
		}
	}
}

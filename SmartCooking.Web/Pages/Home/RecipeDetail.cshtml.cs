using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Recipes;

namespace SmartCooking.Web.Pages.Home
{
    public class RecipeDetailModel : PageModel
    {
		private readonly IRecipeRepository recipeRepository;
		private readonly IRecipeImageRepository recipeImageRepository;

		public RecipeHeader RecipeHeader { get; set; }
		public IEnumerable<RecipeDetail> RecipeDetails { get; set; }
		public IEnumerable<RecipeImage> RecipeImages { get; set; }

		public RecipeDetailModel(IRecipeRepository recipeRepository, IRecipeImageRepository recipeImageRepository)
		{
			this.recipeRepository = recipeRepository;
			this.recipeImageRepository = recipeImageRepository;
		}

        public async Task<IActionResult> OnGetAsync(int? recipeId)
        {
			if (!recipeId.HasValue)
			{
				return RedirectToPage(Url.Content($"~/Home/Index"));
			}

			RecipeHeader = await recipeRepository.GetRecipeHeader(recipeId.Value);

			if(RecipeHeader is null)
			{
				return RedirectToPage(Url.Content($"~/Home/Index"));
			}

			RecipeDetails = await recipeRepository.GetRecipeDetails(recipeId.Value);

			RecipeImages = await recipeImageRepository.GetRecipeImages(recipeId.Value);
			ViewData["Title"] = RecipeHeader.Name;
			return Page();
        }
    }
}

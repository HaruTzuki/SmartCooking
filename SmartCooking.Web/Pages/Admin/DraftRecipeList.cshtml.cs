using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Recipes;
using SmartCooking.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class DraftRecipeListModel : AdminPageModel
	{
		private readonly IRecipeRepository recipeRepository;

		[BindProperty] public IEnumerable<RecipeHeader> Recipes { get; set; }

		public DraftRecipeListModel(IRecipeRepository recipeRepository)
		{
			this.recipeRepository = recipeRepository;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			Recipes = (await recipeRepository.GetRecipeHeaders()).Where(x => x.RecipeType == Common.Enumeration.RecipeType.Draft);

			if (Recipes is null)
			{
				return RedirectToPage(Url.Content("~/Admin/"));
			}

			return Page();
		}

		public async Task<IActionResult> OnPostDelete(int? recipeId)
		{
			if (!recipeId.HasValue)
			{
				HasError = true;
				ViewData["Error"] = "Δεν μπορείτε να περάσετε κενό ID.";
				return Page();
			}

			RecipeHeader dbRecipeHeader = await recipeRepository.GetRecipeHeader(recipeId.Value);

			if (dbRecipeHeader is null)
			{
				HasError = true;
				ViewData["Error"] = "Δεν υπάρχει εγγραφή με το ID που δόθηκε.";
				return Page();
			}

			IEnumerable<RecipeDetail> dbRecipeDetails = await recipeRepository.GetRecipeDetails(dbRecipeHeader.Id);

			foreach (RecipeDetail recipeDetail in dbRecipeDetails)
			{
				if (!await recipeRepository.DeleteRecipeDetail(recipeDetail))
				{
					HasError = true;
					ViewData["Error"] = "Δεν μπορέσαμε να σβήσουμε την εγγραφή σας.";
					return Page();
				}
			}

			if (!await recipeRepository.DeleteRecipeHeader(dbRecipeHeader))
			{
				HasError = true;
				ViewData["Error"] = "Δεν μπορέσαμε να σβήσουμε την εγγραφή σας.";
				return Page();
			}

			TempData["SuccessMessage"] = "Η διαγραφή του στοιχείου έγινε με επιτυχία.";

			return RedirectToPage(Url.Content("~/Admin/RecipeList"));
		}
	}
}
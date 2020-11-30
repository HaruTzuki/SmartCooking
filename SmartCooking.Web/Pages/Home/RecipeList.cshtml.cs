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
    public class RecipeListModel : PageModel
    {
		private readonly IRecipeRepository recipeRepository;
        [BindProperty] public IEnumerable<RecipeHeader> RecipeHeaders { get; set; }

		public RecipeListModel(IRecipeRepository recipeRepository)
		{
			this.recipeRepository = recipeRepository;
		}

        public async Task<IActionResult> OnGetAsync()
        {
            RecipeHeaders = (await recipeRepository.GetRecipeHeaders()).Where(x=>x.RecipeType == Common.Enumeration.RecipeType.Done);

            return Page();
        }
    }
}

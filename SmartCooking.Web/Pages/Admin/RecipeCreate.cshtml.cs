using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Infastructure.Recipes;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages.Admin
{
	public class RecipeCreateModel : AdminPageModel
	{
		private readonly IRecipeRepository recipeRepository;
		private readonly IItemRepository itemRepository;
		private readonly IUnitRepository unitRepository;

		[BindProperty] public RecipeHeader RecipeHeader { get; set; }
		[BindProperty] public IEnumerable<RecipeDetail> RecipeDetails { get; set; }
		[BindProperty] public IEnumerable<Unit> UnitsList { get; set; }
		[BindProperty] public IEnumerable<Item> ItemsList { get; set; }

		public RecipeCreateModel(IRecipeRepository recipeRepository, IItemRepository itemRepository, IUnitRepository unitRepository)
		{
			this.recipeRepository = recipeRepository;
			this.itemRepository = itemRepository;
			this.unitRepository = unitRepository;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			await InitializeLists(GetOptions.All);

			return Page();
		}

		private async Task InitializeLists(GetOptions getOptions)
		{
			if (getOptions == GetOptions.All || getOptions == GetOptions.Items)
			{
				ItemsList = await itemRepository.GetItems();
			}

			if (getOptions == GetOptions.All || getOptions == GetOptions.Units)
			{
				UnitsList = await unitRepository.GetUnits();
			}

		}

		public async Task<IActionResult> OnGetSearchItem(string term)
		{
			await InitializeLists(GetOptions.Items);
			var result = ItemsList.Where(x => x.Name.ToLower().Contains(term.ToLower())).Select(x => x.Name ?? "");
			return new JsonResult(result);
		}

		public async Task<IActionResult> OnGetSearchUnit(string term)
		{
			await InitializeLists(GetOptions.Units);
			var result = UnitsList.Where(x => x.Name.ToLower().Contains(term.ToLower())).Select(x => x.Name ?? "");
			return new JsonResult(result);
		}

		public enum GetOptions
		{
			All,
			Items,
			Units
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Web.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class IndexModel : AdminPageModel
	{
		private readonly IItemRepository itemRepository;
		private readonly IItemCategoryRepository itemCategoryRepository;
		private readonly IRecipeRepository recipeRepository;
		private readonly IUnitRepository unitRepository;

		public int ItemCount { get; set; }
		public int ItemCategoryCount { get; set; }
		public int RecipeCount { get; set; }
		public int UnitCount { get; set; }

		public IndexModel(IItemRepository itemRepository, IItemCategoryRepository itemCategoryRepository, IRecipeRepository recipeRepository, IUnitRepository unitRepository)
		{
			this.itemRepository = itemRepository;
			this.itemCategoryRepository = itemCategoryRepository;
			this.recipeRepository = recipeRepository;
			this.unitRepository = unitRepository;
		}

		public async Task<IActionResult> OnGet()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			ItemCount = (await itemRepository.GetItems()).Count();
			ItemCategoryCount = (await itemCategoryRepository.GetItemCategories()).Count();
			RecipeCount = (await recipeRepository.GetRecipeHeaders()).Count();
			UnitCount = (await unitRepository.GetUnits()).Count();

			return Page();
		}
	}
}
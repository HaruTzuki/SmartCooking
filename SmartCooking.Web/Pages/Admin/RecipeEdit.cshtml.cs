using Microsoft.AspNetCore.Mvc;
using SmartCooking.Common.Extensions;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Infastructure.Recipes;
using SmartCooking.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class RecipeEditModel : AdminPageModel
	{
		private readonly IRecipeRepository recipeRepository;
		private readonly IItemRepository itemRepository;
		private readonly IUnitRepository unitRepository;

		[BindProperty] public int DraftRecipeHeaderId { get; set; }
		[BindProperty] public RecipeHeader RecipeHeader { get; set; }
		[BindProperty] public IEnumerable<RecipeDetail> RecipeDetails { get; set; }
		[BindProperty] public IEnumerable<Unit> UnitsList { get; set; }
		[BindProperty] public IEnumerable<Item> ItemsList { get; set; }

		public RecipeEditModel(IRecipeRepository recipeRepository, IItemRepository itemRepository, IUnitRepository unitRepository)
		{
			this.recipeRepository = recipeRepository;
			this.itemRepository = itemRepository;
			this.unitRepository = unitRepository;
		}

		public async Task<IActionResult> OnGetAsync(int? draftHeaderId)
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}


			if (draftHeaderId.HasValue)
			{
				RecipeHeader = await recipeRepository.GetRecipeHeader(draftHeaderId.Value);
				RecipeDetails = await recipeRepository.GetRecipeDetails(RecipeHeader.Id);
				this.DraftRecipeHeaderId = draftHeaderId.Value;
			}

			if (RecipeHeader is null)
			{
				RecipeHeader = new RecipeHeader
				{
					RecipeType = Common.Enumeration.RecipeType.Draft
				};
				await recipeRepository.InsertRecipeHeader(RecipeHeader);

				return RedirectToPage("RecipeCreate", new { draftHeaderId = RecipeHeader.Id });
			}

			await InitializeLists(GetOptions.All);

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			this.RecipeHeader.RecipeType = Common.Enumeration.RecipeType.Done;

			if (await recipeRepository.UpdateRecipeHeader(this.RecipeHeader))
			{
				foreach (var recipeDetail in RecipeDetails)
				{
					if (!await recipeRepository.UpdateRecipeDetail(recipeDetail))
					{
						HasError = true;
						ViewData["Error"] = "Κάτι πήγε στράβα και δεν μπορεί να αποθηκευτεί η εγγραφή.";
						return Page();
					}
				}

				TempData["SuccessMessage"] = "Η συνταγή ενημερώθηκε με επιτυχία.";
				return RedirectToPage(Url.Content("~/Admin/RecipeList"));
			}

			HasError = true;
			ViewData["Error"] = "Κάτι πήγε στράβα και δεν μπορεί να αποθηκευτεί η εγγραφή.";
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

		public async Task<IActionResult> OnGetAddToList(string itemName, string unitName, string qty, string recipeId)
		{
			try
			{
				var itemObj = new Item();
				var unitObj = new Unit();

				var itemList = await itemRepository.GetItems();
				var unitList = await unitRepository.GetUnits();


				if (!itemList.Any(x => x.Name.ToLower().StartsWith(itemName.ToLower().Trim())))
				{
					itemObj.Name = itemName.Trim();
					await itemRepository.InsertItem(itemObj);
				}
				else
				{
					itemObj = itemList.FirstOrDefault(x => x.Name.ToLower().StartsWith(itemName.ToLower().Trim()));
				}


				if (!unitList.Any(x => x.Name.ToLower().StartsWith(unitName.ToLower().Trim())))
				{
					unitObj.Name = unitName.Trim();
					await unitRepository.InsertUnit(unitObj);
				}
				else
				{
					unitObj = unitList.FirstOrDefault(x => x.Name.ToLower().StartsWith(unitName.ToLower().Trim()));
				}


				RecipeDetail recipeDetail = new RecipeDetail()
				{
					ItemId = itemObj.Id,
					UnitId = unitObj.Id,
					Quantity = float.Parse(qty),
					RecipeHeaderId = recipeId.ToInt()
				};


				await recipeRepository.InsertRecipeDetail(recipeDetail);

				return new JsonResult(new { result = true });
			}
			catch (Exception ex)
			{
				return new JsonResult(new { result = false, message = ex.Message });
			}
		}

		public enum GetOptions
		{
			All,
			Items,
			Units
		}
	}
}

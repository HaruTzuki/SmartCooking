using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class ItemCategoryListModel : AdminPageModel
	{
		private readonly IItemCategoryRepository itemCategoryRepository;
		[BindProperty] public IEnumerable<ItemCategory> ItemCategories { get; set; }


		public ItemCategoryListModel(IItemCategoryRepository itemCategoryRepository)
		{
			this.itemCategoryRepository = itemCategoryRepository;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			ItemCategories = await itemCategoryRepository.GetItemCategories();

			if (ItemCategories is null)
			{
				return RedirectToPage(Url.Content("~/Admin/"));
			}

			return Page();
		}


		public async Task<IActionResult> OnPostDelete(int? itemCategoryId)
		{
			if (!itemCategoryId.HasValue)
			{
				HasError = true;
				ViewData["Error"] = "Δεν μπορείτε να περάσετε κενό ID.";
				return Page();
			}

			ItemCategory dbItemCategory = await itemCategoryRepository.GetItemCategory(itemCategoryId.Value);

			if (dbItemCategory is null)
			{
				HasError = true;
				ViewData["Error"] = "Δεν υπάρχει εγγραφή με το ID που δόθηκε.";
				return Page();
			}

			if (!await itemCategoryRepository.DeleteItemCategory(dbItemCategory))
			{
				HasError = true;
				ViewData["Error"] = "Δεν μπορέσαμε να σβήσουμε την εγγραφή σας.";
				return Page();
			}

			TempData["SuccessMessage"] = "Η διαγραφή του στοιχείου έγινε με επιτυχία.";

			return RedirectToPage(Url.Content("~/Admin/ItemCategoryList"));
		}

	}
}

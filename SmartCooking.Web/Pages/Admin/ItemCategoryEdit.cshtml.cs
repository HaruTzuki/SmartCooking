using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Web.Helpers;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class ItemCategoryEditModel : AdminPageModel
	{
		private readonly IItemCategoryRepository itemCategoryRepository;
		[BindProperty] public ItemCategory ItemCategory { get; set; }
		[BindProperty] public int ItemCategoryId { get; set; }

		public ItemCategoryEditModel(IItemCategoryRepository itemCategoryRepository)
		{
			this.itemCategoryRepository = itemCategoryRepository;
		}

		public async Task<IActionResult> OnGetAsync(int? itemCategoryId)
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			if (!itemCategoryId.HasValue)
			{
				return RedirectToPage(Url.Content("~/Admin/ItemCategoryList"));
			}

			ItemCategory = await itemCategoryRepository.GetItemCategory(itemCategoryId.Value);
			ItemCategoryId = itemCategoryId.Value;

			if (ItemCategory == null)
			{
				return RedirectToPage(Url.Content("~/Admin/ItemCategoryList"));
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (await itemCategoryRepository.UpdateItemCategory(ItemCategory))
			{
				TempData["SuccessMessage"] = "Η κατηγορία ενημερώθηκε με επιτυχία.";
				return RedirectToPage(Url.Content("~/Admin/ItemCategoryList"));
			}

			HasError = true;
			ViewData["Error"] = "Κάτι πήγε στραβά και δεν μπορεί να αποθηκευτεί η εγγραφή.";
			return Page();
		}
	}
}
using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Web.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class ItemCategoryCreateModel : AdminPageModel
	{
		private readonly IItemCategoryRepository itemCategoryRepository;
		[BindProperty] public ItemCategory ItemCategory { get; set; }

		public ItemCategoryCreateModel(IItemCategoryRepository itemCategoryRepository)
		{
			this.itemCategoryRepository = itemCategoryRepository;
		}

		public IActionResult OnGet()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if ((await itemCategoryRepository.GetItemCategories()).Any(x => x.Name.ToLower() == ItemCategory.Name.ToLower()))
			{
				HasError = true;
				ViewData["Error"] = "Η κατηγορία υπάρχει ήδη. Παρακαλώ επιλέξτε κάποια άλλη περιγραφή";
				return Page();
			}

			if (await itemCategoryRepository.InsertItemCategory(ItemCategory))
			{
				HasError = false;
				TempData["SuccessMessage"] = "Η κατηγορία προστέθηκε με επιτυχία.";
				return RedirectToPage(Url.Content("~/Admin/ItemCategoryList"));
			}

			HasError = true;
			ViewData["Error"] = "Κάτι πήγε στραβά και δεν μπορεί να αποθηκευτεί η εγγραφή.";
			return Page();
		}
	}
}
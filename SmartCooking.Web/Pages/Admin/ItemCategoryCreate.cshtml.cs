using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages.Admin
{
    public class ItemCategoryCreateModel : AdminPageModel
    {
		private readonly IItemCategoryRepository itemCategoryRepository;
        [BindProperty] public ItemCategory ItemCategory { get; set; }
		public bool IsErrorInRegister { get; set; } = false;

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
			if((await itemCategoryRepository.GetItemCategories()).Any(x=>x.Name.ToLower() == ItemCategory.Name.ToLower()))
			{
				IsErrorInRegister = true;
				ViewData["Error"] = "Η κατηγορία υπάρχει ήδη. Παρακαλώ επιλέξτε κάποια άλλη περιγραφή";
				return Page();
			}

			if(await itemCategoryRepository.InsertItemCategory(ItemCategory))
			{
				TempData["SuccessMessage"] = "Η κατηγορία προστέθηκε με επιτυχία.";
				return RedirectToPage(Url.Content("~/Admin/ItemCategoryList"));
			}

			IsErrorInRegister = true;
			ViewData["Error"] = "Κάτι πήγε στραβά και δεν μπορεί να αποθηκευτεί ή εγγραφή";
			return Page();
		}
    }
}

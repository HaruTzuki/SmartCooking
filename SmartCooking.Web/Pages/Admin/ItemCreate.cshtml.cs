using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class ItemCreateModel : AdminPageModel
	{
		private readonly IItemRepository itemRepository;
		private readonly IItemCategoryRepository itemCategoryRepository;

		[BindProperty] public Item Item { get; set; }
		[BindProperty] public IEnumerable<ItemCategory> ItemCategories { get; set; }
		[BindProperty] public List<SelectListItem> SelectListItems { get; set; } = new List<SelectListItem>();

		public ItemCreateModel(IItemRepository itemRepository, IItemCategoryRepository itemCategoryRepository)
		{
			this.itemRepository = itemRepository;
			this.itemCategoryRepository = itemCategoryRepository;
		}

		public async Task<IActionResult> OnGet()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			ItemCategories = await itemCategoryRepository.GetItemCategories();
			ItemCategories.ToList().ForEach(x =>
			{
				SelectListItems.Add(new SelectListItem(x.Name, x.Id.ToString()));
			});

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if ((await itemRepository.GetItems()).Any(x => x.Name.ToLower() == Item.Name.ToLower()))
			{
				HasError = true;
				ViewData["Error"] = "Το είδος υπάρχει ήδη. Παρακαλώ επιλέξτε κάποια άλλη περιγραφή.";
				return Page();
			}

			if (await itemRepository.InsertItem(Item))
			{
				TempData["SuccessMessage"] = "Το είδος προστέθηκε με επιτυχία.";
				return RedirectToPage(Url.Content("~/Admin/ItemList"));
			}

			HasError = true;
			ViewData["Error"] = "Κάτι πήγε στραβά και δεν μπορεί να αποθηκευτή η εγγραφή.";
			return Page();
		}
	}
}
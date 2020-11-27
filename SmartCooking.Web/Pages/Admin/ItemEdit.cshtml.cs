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
	public class ItemEditModel : AdminPageModel
	{
		private readonly IItemRepository itemRepository;
		private readonly IItemCategoryRepository itemCategoryRepository;

		[BindProperty] public Item Item { get; set; }
		[BindProperty] public IEnumerable<ItemCategory> ItemCategories { get; set; }
		[BindProperty] public List<SelectListItem> SelectListItems { get; set; } = new List<SelectListItem>();

		public ItemEditModel(IItemRepository itemRepository, IItemCategoryRepository itemCategoryRepository)
		{
			this.itemRepository = itemRepository;
			this.itemCategoryRepository = itemCategoryRepository;
		}

		public async Task<IActionResult> OnGetAsync(int? itemId)
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			if (!itemId.HasValue)
			{
				return RedirectToPage(Url.Content("~/Admin/ItemList"));
			}

			Item = await itemRepository.GetItem(itemId.Value);

			if (Item is null)
			{
				return RedirectToPage(Url.Content("~/Admin/ItemList"));
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
			if (await itemRepository.UpdateItem(Item))
			{
				TempData["SuccessMessage"] = "Το είδος ενημερώθηκε με επιτυχία.";
				return RedirectToPage(Url.Content("~/Admin/ItemList"));
			}

			HasError = true;
			ViewData["Error"] = "Κάτι πήγε στραβά και δεν μπορεί να αποθηκευτεί ή εγγραφή";
			return Page();
		}
	}
}
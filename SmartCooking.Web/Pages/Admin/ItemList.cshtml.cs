using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class ItemListModel : AdminPageModel
	{
		private readonly IItemRepository itemRepository;
		private readonly IItemCategoryRepository itemCategoryRepository;

		[BindProperty] public IEnumerable<Item> Items { get; set; }
		[BindProperty] public IEnumerable<ItemCategory> ItemCategories { get; set; }


		public ItemListModel(IItemRepository itemRepository, IItemCategoryRepository itemCategoryRepository)
		{
			this.itemRepository = itemRepository;
			this.itemCategoryRepository = itemCategoryRepository;
		}
		public async Task<IActionResult> OnGetAsync()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			Items = await itemRepository.GetItems();
			ItemCategories = await itemCategoryRepository.GetItemCategories();

			if (Items is null)
			{
				return RedirectToPage(Url.Content("~/Admin/"));
			}

			return Page();
		}
		public async Task<IActionResult> OnPostDelete(int? itemId)
		{
			if (!itemId.HasValue)
			{
				HasError = true;
				ViewData["Error"] = "��� �������� �� �������� ���� ID.";
				return Page();
			}

			var dbItem = await itemRepository.GetItem(itemId.Value);

			if (dbItem is null)
			{
				HasError = true;
				ViewData["Error"] = "��� ������� ������� �� �� ID ��� ������.";
				return Page();
			}

			if (!await itemRepository.DeleteItem(dbItem))
			{
				HasError = true;
				ViewData["Error"] = "��� ��������� �� �������� ��� ������� ���.";
				return Page();
			}

			TempData["SuccessMessage"] = "� �������� ��� ��������� ����� �� ��������.";

			return RedirectToPage(Url.Content("~/Admin/ItemList"));
		}
	}
}

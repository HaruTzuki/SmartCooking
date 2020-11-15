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

			if(Items is null)
			{
				return RedirectToPage(Url.Content("~/Admin/"));
			}

			return Page();
        }
    }
}

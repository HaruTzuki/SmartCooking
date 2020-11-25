using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Web.Helpers;

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

			if(ItemCategory == null)
			{
				return RedirectToPage(Url.Content("~/Admin/ItemCategoryList"));
			}

			return Page();
        }

		public async Task<IActionResult> OnPostAsync()
		{
			if (await itemCategoryRepository.UpdateItemCategory(ItemCategory))
			{
				TempData["SuccessMessage"] = "� ��������� ����������� �� ��������.";
				return RedirectToPage(Url.Content("~/Admin/ItemCategoryList"));
			}

			HasError = true;
			ViewData["Error"] = "���� ���� ������ ��� ��� ������ �� ����������� � �������.";
			return Page();
		}

    }
}

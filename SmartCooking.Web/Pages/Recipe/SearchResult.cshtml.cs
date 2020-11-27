using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Recipe
{
	public class SearchResultModel : PageModel
	{
		private readonly IItemRepository itemRepository;

		public SearchResultModel(IItemRepository itemRepository)
		{
			this.itemRepository = itemRepository;
		}

		[BindProperty(SupportsGet = true)]
		public List<Item> Items { get; set; }

		public async Task<IActionResult> OnGetAsync(string query)
		{
			string[] Splitted = query.Split("&");

			foreach (string str in Splitted)
			{
				Items.Add(await itemRepository.GetItem(str));
			}

			/*
            Να βρούμε συνταγές που περιέχουν 1 ή περισσότερα είδη.
             */

			return Page();
		}
	}
}
using Microsoft.AspNetCore.Mvc;
using SmartCooking.Common.Serialization.Json;
using SmartCooking.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class ItemController : Controller
	{
		private readonly IItemRepository itemRepository;

		public ItemController(IItemRepository itemRepository)
		{
			this.itemRepository = itemRepository;
		}

		[HttpGet("GetItems")]
		public async Task<JsonResult> GetItems()
		{
			return new JsonResult(await itemRepository.GetItems());
		}

		[HttpGet("SearchItemByName")]
		public async Task<JsonResult> SearchItemByName(string term)
		{
			if (string.IsNullOrEmpty(term))
			{
				return new JsonResult((await itemRepository.GetItems()).Take(10).Select(itm => itm.Name));
			}

			IEnumerable<string> result = (await itemRepository.GetItems())
										.Where(itm => itm.Name.ToLower()
																.Contains(term.ToLower().Trim()))
										.OrderBy(itm=>itm.Name)
										.Select(itm=>itm.Name);

			return new JsonResult(result);
		}
	}
}

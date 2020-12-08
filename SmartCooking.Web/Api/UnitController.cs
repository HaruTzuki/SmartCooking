using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class UnitController : Controller
	{
		private readonly IUnitRepository unitRepository;

		public UnitController(IUnitRepository unitRepository)
		{
			this.unitRepository = unitRepository;
		}

		[HttpGet("GetUnits")]
		public async Task<JsonResult> GetUnits()
		{
			return new JsonResult(await unitRepository.GetUnits());
		}

		[HttpGet("SearchUnitByName")]
		public async Task<JsonResult> SearchUnitByName(string term)
		{
			if (string.IsNullOrEmpty(term))
			{
				return new JsonResult((await unitRepository.GetUnits()).Take(10).Select(unt => unt.Name));
			}

			IEnumerable<string> result = (await unitRepository.GetUnits())
										.Where(unt => unt.Name.ToLower()
																.Contains(term.ToLower().Trim()))
										.OrderBy(unt=>unt.Name)
										.Select(unt => unt.Name);

			return new JsonResult(result);
		}
	}
}

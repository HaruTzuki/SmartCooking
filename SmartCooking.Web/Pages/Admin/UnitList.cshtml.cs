using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class UnitListModel : AdminPageModel
	{
		private readonly IUnitRepository unitRepository;
		[BindProperty] public IEnumerable<Unit> Units { get; set; }


		public UnitListModel(IUnitRepository unitRepository)
		{
			this.unitRepository = unitRepository;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			Units = await unitRepository.GetUnits();

			if (Units is null)
			{
				return RedirectToPage(Url.Content("~/Admin/"));
			}

			return Page();
		}


		public async Task<IActionResult> OnPostDelete(int? unitId)
		{
			if (!unitId.HasValue)
			{
				HasError = true;
				ViewData["Error"] = "��� �������� �� �������� ���� ID.";
				return Page();
			}

			Unit dbUnit = await unitRepository.GetUnit(unitId.Value);

			if (dbUnit is null)
			{
				HasError = true;
				ViewData["Error"] = "��� ������� ������� �� �� ID ��� ������.";
				return Page();
			}

			if (!await unitRepository.DeleteUnit(dbUnit))
			{
				HasError = true;
				ViewData["Error"] = "��� ��������� �� �������� ��� ������� ���.";
				return Page();
			}

			TempData["SuccessMessage"] = "� �������� ��� ��������� ����� �� ��������.";

			return RedirectToPage(Url.Content("~/Admin/UnitList"));
		}

	}
}

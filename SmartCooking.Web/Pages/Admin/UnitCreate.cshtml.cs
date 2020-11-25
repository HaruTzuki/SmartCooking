using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages.Admin
{
	public class UnitCreateModel : AdminPageModel
    {
		private readonly IUnitRepository unitRepository;
        [BindProperty] public Unit Unit { get; set; }

		public UnitCreateModel(IUnitRepository unitRepository)
		{
			this.unitRepository = unitRepository;
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
			if((await unitRepository.GetUnits()).Any(x=>x.Name.ToLower() == Unit.Name.ToLower()))
			{
				HasError = true;
				ViewData["Error"] = "� ������ �������� ������� ���. �������� �������� ������ ���� ���������";
				return Page();
			}

			if(await unitRepository.InsertUnit(Unit))
			{
				TempData["SuccessMessage"] = "� ������ �������� ���������� �� ��������.";
				return RedirectToPage(Url.Content("~/Admin/UnitList"));
			}

			HasError = true;
			ViewData["Error"] = "���� ���� ������ ��� ��� ������ �� ����������� � �������.";
			return Page();
		}
    }
}

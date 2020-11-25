using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Web.Helpers;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class UnitEditModel : AdminPageModel
	{
		private readonly IUnitRepository unitRepository;
		[BindProperty] public Unit Unit { get; set; }
		public UnitEditModel(IUnitRepository unitRepository)
		{
			this.unitRepository = unitRepository;
		}

		public async Task<IActionResult> OnGetAsync(int? unitId)
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			if (!unitId.HasValue)
			{
				return RedirectToPage(Url.Content("~/Admin/UnitList"));
			}

			Unit = await unitRepository.GetUnit(unitId.Value);

			if (Unit == null)
			{
				return RedirectToPage(Url.Content("~/Admin/UnitList"));
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (await unitRepository.UpdateUnit(Unit))
			{
				TempData["SuccessMessage"] = "Η μόνάδα μέτρησης ενημερώθηκε με επιτυχία.";
				return RedirectToPage(Url.Content("~/Admin/UpdateList"));
			}

			HasError = true;
			ViewData["Error"] = "Κάτι πήγε στραβά και δεν μπορεί να αποθηκευτεί ή εγγραφή";
			return Page();
		}

	}
}

using Microsoft.AspNetCore.Mvc;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages.Admin
{
	public class IndexModel : AdminPageModel
	{
		public IActionResult OnGet()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			return Page();
		}
	}
}

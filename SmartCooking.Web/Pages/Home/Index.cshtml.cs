using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages
{
	public class IndexModel : UserPageModel
	{
		public IndexModel(IUserRepository userRepository) : base (userRepository)
		{

		}

		public IActionResult OnGet()
		{
			GetSessionValues();
			return Page();
		}
	}
}
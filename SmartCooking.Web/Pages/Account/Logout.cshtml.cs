using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages.Account
{
	public class LogoutModel : UserPageModel
    {
        public IActionResult OnGet()
        {
            if(HttpContext.Session.Keys.Contains("USERNAME"))
			{
                HttpContext.Session.Remove("USERNAME");
			}

			if (HttpContext.Session.Keys.Contains("ISADMIN"))
			{
                HttpContext.Session.Remove("ISADMIN");
			}

			if (HttpContext.Session.Keys.Contains("ISLOGIN"))
			{
                HttpContext.Session.Remove("ISLOGIN");
			}

            return RedirectToPage(Url.Content("~/Home/Index"));
        }
    }
}

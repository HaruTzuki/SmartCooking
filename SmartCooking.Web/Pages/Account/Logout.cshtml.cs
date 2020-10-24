using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartCooking.Web.Pages.Account
{
	public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            if(HttpContext.Session.Keys.Contains("Username"))
			{
                HttpContext.Session.Remove("Username");
			}

			if (HttpContext.Session.Keys.Contains("IsAdmin"))
			{
                HttpContext.Session.Remove("IsAdmin");
			}

			if (HttpContext.Session.Keys.Contains("IsLogged"))
			{
                HttpContext.Session.Remove("ISLOGIN");
			}

            HttpContext.Session.Clear();

            return RedirectToPage("/Index");
        }
    }
}

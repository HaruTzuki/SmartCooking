using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages
{
	public class IndexModel : UserPageModel
    {
        public IActionResult OnGet()
        {
            ViewData["IsLogged"] = HttpContext.Session.GetInt32("ISLOGIN");
            ViewData["Username"] = HttpContext.Session.GetString("USERNAME");
            ViewData["IsAdmin"] = HttpContext.Session.GetString("ISADMIN");

            return Page();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace SmartCooking.Web.Helpers
{
	public class AdminPageModel : PageModel
	{
		public bool CheckPermissions()
		{
			if (HttpContext.Session.Keys.Contains("ISADMIN")){
				if (HttpContext.Session.GetString("ISADMIN") != "1")
				{
					return false;
				}
			}
			else
			{
				return false;
			}

			return true;
		}
	}
}

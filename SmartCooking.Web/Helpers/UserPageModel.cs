using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using SmartCooking.Infastructure.Security;
using SmartCooking.Data.Repository;
using System.Threading.Tasks;
using System.Linq;

namespace SmartCooking.Web.Helpers
{
	public class UserPageModel : PageModel
	{
		private readonly IUserRepository userRepository;

		public bool HasError { get; set; }
		public User CurrentUser { get; set; }

		public UserPageModel(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}


		public bool IsUserLogged()
		{
			if (HttpContext.Session.GetInt32("ISLOGIN") == 1)
			{
				CurrentUser = userRepository.GetUser(HttpContext.Session.GetInt32("USERID").Value).Result;
				return true;
			}
			else
			{
				CurrentUser = null;
				return false;
			}
		}

		public void GetSessionValues()
		{
			ViewData["IsLogged"] = HttpContext.Session.GetInt32("ISLOGIN");
			ViewData["Username"] = HttpContext.Session.GetString("USERNAME");
			ViewData["IsAdmin"] = HttpContext.Session.GetString("ISADMIN");
		}
	}
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCooking.Common.Cryptography;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;
using SmartCooking.Web.Helpers;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Account
{
	public class LoginModel : UserPageModel
	{
		[BindProperty] public new User User { get; set; }

		private readonly IUserRepository userRepository;

		public LoginModel(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task<IActionResult> OnPostAsync()
		{
			User dbUser = (await userRepository.GetUsers()).FirstOrDefault(x => x.Username.ToLower() == User.Username.ToLower());

			if (dbUser is null)
			{
				HasError = true;
				ViewData["Error"] = "Ο χρήστης δεν υπάρχει.";
				return Page();
			}

			if (!CrypterAlgorithm.VerifyHash(SHA256.Create(), User.Password, dbUser.Password))
			{
				HasError = true;
				ViewData["Error"] = "Δεν ταιριάζει ο κωδικός χρήστης.";
				return Page();
			}

			HttpContext.Session.SetString("USERNAME", dbUser.Username);
			HttpContext.Session.SetInt32("ISLOGIN", 1);
			HttpContext.Session.SetString("ISADMIN", dbUser.IsAdmin ? "1" : "0");

			return RedirectToPage(Url.Content("~/Home/Index"));
		}
	}
}
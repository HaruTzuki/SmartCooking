using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartCooking.Common.Cryptography;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;

namespace SmartCooking.Web.Pages.Account
{
	public class LoginModel : PageModel
	{
		[BindProperty] public new User User { get; set; }

		private readonly IUserRepository userRepository;

		public LoginModel(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var dbUser = (await userRepository.GetUsers()).FirstOrDefault(x => x.Username.ToLower() == User.Username.ToLower());

			if (!CrypterAlgorithm.VerifyHash(SHA256.Create(), User.Password, dbUser.Password))
			{
				return Page();
			}

			HttpContext.Session.SetString("USERNAME", dbUser.Username);
			HttpContext.Session.SetInt32("ISLOGIN", 1);
			HttpContext.Session.SetString("ISADMIN", dbUser.IsAdmin ? "1" : "0");

			return RedirectToPage("/Home/Index");
		}
	}
}

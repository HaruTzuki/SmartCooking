using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;

namespace SmartCooking.Web.Pages.Account
{
	public class RegisterModel : PageModel
	{
		public User User { get; set; }
		private readonly IUserRepository userRepository;

		public RegisterModel(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
			User = new User();
		}
		
		public async Task<IActionResult> OnPostAsync()
		{
			await Task.Delay(1);

			return RedirectToPage("./Index");
		}
	}
}

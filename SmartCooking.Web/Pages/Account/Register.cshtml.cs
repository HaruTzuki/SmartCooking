using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Internal;
using SmartCooking.Common.Cryptography;
using SmartCooking.Common.Extensions;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;

namespace SmartCooking.Web.Pages.Account
{
	public class RegisterModel : PageModel
	{
		[BindProperty] public User User { get; set; } = new User();
		public bool IsErrorInRegister = false;
		private readonly IUserRepository userRepository;

		public RegisterModel(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		
		public async Task<IActionResult> OnPostAsync()
		{

			var dbUser = await userRepository.GetUsers();
			
			if(dbUser.AsEnumerable().Any(x=>x.Username.ToLower() == User.Username.ToLower()))
			{
				IsErrorInRegister = true;
				ViewData["Error"] = "Σφάλμα! Υπάρχει ήδη χρήστης με αυτό το Όνομα Χρήστη που επιλέξατε.";
				return Page();
			}

			if(dbUser.AsEnumerable().Any(x=>x.Email.ToLower() == User.Email.ToLower()))
			{
				IsErrorInRegister = true;
				ViewData["Error"] = "Σφάλμα! Υπάρχει ήδη χρήστης με αυτό το Ηλ. Ταχυδρομείο που επιλέξατε.";
				return Page();
			}


			User.Password = CrypterAlgorithm.ComputeSha256Hash(User.Password);

			if(await userRepository.InsertUser(User))
			{
				return RedirectToPage("/Index");
			}

			return Page();
		}
	}
}

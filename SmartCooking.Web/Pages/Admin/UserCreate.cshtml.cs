using Microsoft.AspNetCore.Mvc;
using SmartCooking.Common.Cryptography;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;
using SmartCooking.Web.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class UserCreateModel : AdminPageModel
	{
		private readonly IUserRepository userRepository;

		[BindProperty] public new User User { get; set; }

		public UserCreateModel(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public IActionResult OnGet()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			System.Collections.Generic.IEnumerable<User> dbUsers = await userRepository.GetUsers();

			if (dbUsers.Any(x => x.Username.ToLower() == User.Username.ToLower().Trim()))
			{
				HasError = true;
				ViewData["Error"] = "Υπάρχει ήδη χρήστης με Όνομα Χρήστη (Username) που δώσατε.";
				return Page();
			}

			if (dbUsers.Any(x => x.Email.ToLower() == User.Email.ToLower().Trim()))
			{
				HasError = true;
				ViewData["Error"] = "Υπάρχει ήδη χρήστης με την Ηλεκτρονική Διεύθυνση που δώσατε.";
				return Page();
			}

			User.Username = User.Username.Trim();
			User.Email = User.Email.Trim();
			User.Password = CrypterAlgorithm.ComputeSha256Hash(User.Password);

			if (await userRepository.InsertUser(User))
			{
				HasError = false;
				TempData["SuccessMessage"] = "Ο Χρήστης προστέθηκε με επιτυχία.";
				return RedirectToPage(Url.Content("~/Admin/UserList"));
			}

			HasError = true;
			ViewData["Error"] = "Δεν μπορέσαμε να κάνουμε την εγγραφή προσπαθήστε ξανά";
			return Page();
		}
	}
}

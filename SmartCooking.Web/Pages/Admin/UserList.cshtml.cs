using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;
using SmartCooking.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class UserListModel : AdminPageModel
	{
		private readonly IUserRepository userRepository;
		[BindProperty] public IEnumerable<User> Users { get; set; }

		public UserListModel(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			Users = await userRepository.GetUsers();

			if (Users is null)
			{
				return RedirectToPage(Url.Content("~/Admin/"));
			}

			return Page();
		}

		public async Task<IActionResult> OnPostDelete(int? userId)
		{
			if (!userId.HasValue)
			{
				HasError = true;
				ViewData["Error"] = "Δεν μπορείτε να περάσετε κενό ID.";
				return Page();
			}

			User dbUser = await userRepository.GetUser(userId.Value);

			if (dbUser is null)
			{
				HasError = true;
				ViewData["Error"] = "Δεν υπάρχει εγγραφή με το ID που δόθηκε.";
				return Page();
			}

			if (!await userRepository.DeleteUser(dbUser))
			{
				HasError = true;
				ViewData["Error"] = "Δεν μπορέσαμε να σβήσουμε την εγγραφή σας.";
				return Page();
			}

			TempData["SuccessMessage"] = "Η διαγραφή του στοιχείου έγινε με επιτυχία.";

			return RedirectToPage(Url.Content("~/Admin/UserList"));
		}
	}
}
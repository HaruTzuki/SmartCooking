using Microsoft.AspNetCore.Mvc;
using SmartCooking.Common.Cryptography;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;
using SmartCooking.Web.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Admin
{
	public class UserEditModel : AdminPageModel
	{
		private readonly IUserRepository userRepository;
		[BindProperty] public User SUser { get; set; }

		public UserEditModel(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task<IActionResult> OnGetAsync(int? userId)
		{
			if (!CheckPermissions())
			{
				return RedirectToPage(Url.Content("~/Home/Index"));
			}

			if (!userId.HasValue)
			{
				return RedirectToPage(Url.Content("~/Admin/UserList"));
			}

			SUser = await userRepository.GetUser(userId.Value);

			if (SUser is null)
			{
				return RedirectToPage(Url.Content("~/Admin/UserList"));
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var dbUsers = await userRepository.GetUsers();

			if (dbUsers.Any(x => x.Username.ToLower() == SUser.Username.ToLower().Trim() && x.Id != SUser.Id))
			{
				HasError = true;
				ViewData["Error"] = "������� ��� ������� �� ����� ������ (Username) ��� ������.";
				return Page();
			}

			if (dbUsers.Any(x => x.Email.ToLower() == SUser.Email.ToLower().Trim() && x.Id != SUser.Id))
			{
				HasError = true;
				ViewData["Error"] = "������� ��� ������� �� ��� ����������� ��������� ��� ������.";
				return Page();
			}

			if (string.IsNullOrEmpty(SUser.Password))
			{
				SUser.Password = dbUsers.FirstOrDefault(x => x.Id == SUser.Id).Password;
			}
			else
			{
				SUser.Password = CrypterAlgorithm.ComputeSha256Hash(SUser.Password);
			}

			var dbUser = await userRepository.GetUser(SUser.Id);

			dbUser.Username = SUser.Username;
			dbUser.Password = SUser.Password;
			dbUser.Email = SUser.Email;
			dbUser.FamilyName = SUser.FamilyName;
			dbUser.IsAdmin = SUser.IsAdmin;

			if (await userRepository.UpdateUser(dbUser))
			{
				HasError = false;
				TempData["SuccessMessage"] = "� ������� ���������� �� ��������.";
				return RedirectToPage(Url.Content("~/Admin/UserList"));
			}

			HasError = true;
			ViewData["Error"] = "��� ��������� �� ������� ��� ������� ����������� ����";
			return Page();
		}
	}
}

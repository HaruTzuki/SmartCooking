using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartCooking.Common.Cryptography;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages.Account
{
	public class RegisterModel : UserPageModel
	{
		[BindProperty] public new User User { get; set; }
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
				HasError = true;
				ViewData["Error"] = "������! ������� ��� ������� �� ���� �� ����� ������ ��� ���������.";
				return Page();
			}

			if(dbUser.AsEnumerable().Any(x=>x.Email.ToLower() == User.Email.ToLower()))
			{
				HasError = true;
				ViewData["Error"] = "������! ������� ��� ������� �� ���� �� ��. ����������� ��� ���������.";
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

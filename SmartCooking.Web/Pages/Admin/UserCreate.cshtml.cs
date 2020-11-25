using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartCooking.Common.Cryptography;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;
using SmartCooking.Web.Helpers;

namespace SmartCooking.Web.Pages.Admin
{
    public class UserCreateModel : AdminPageModel
    {
		private readonly IUserRepository userRepository;

		[BindProperty] public User User { get; set; }

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
            var dbUsers = await userRepository.GetUsers();

            if (dbUsers.Any(x => x.Username.ToLower() == User.Username.ToLower().Trim()))
            {
                HasError = true;
                ViewData["Error"] = "������� ��� ������� �� ����� ������ (Username) ��� ������.";
                return Page();
            }

			if (dbUsers.Any(x => x.Email.ToLower() == User.Email.ToLower().Trim()))
			{
                HasError = true;
                ViewData["Error"] = "������� ��� ������� �� ��� ����������� ��������� ��� ������.";
                return Page();
			}

            User.Username = User.Username.Trim();
            User.Email = User.Email.Trim();
            User.Password = CrypterAlgorithm.ComputeSha256Hash(User.Password);

            if (await userRepository.InsertUser(User))
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

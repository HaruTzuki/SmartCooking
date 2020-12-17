using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Domain;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Helpers
{
	public class CustomController : Controller
	{
		private readonly IUserRepository userRepository;

		public bool HasError { get; set; }
		public string ErrorMessage { get; set; }
		public string SuccessMessage { get; set; }
		public User CurrentUser
		{
			get; set;
		}

		public CustomController(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public void SetUser()
		{
			if (HttpContext.Session.GetInt32("ISLOGIN") == 1)
			{
				CurrentUser = userRepository.GetUser(HttpContext.Session.GetInt32("USERID").Value).Result;
			}
		}
	}
}

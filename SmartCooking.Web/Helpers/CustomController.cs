using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Helpers
{
	public class CustomController : Controller
	{
		public bool HasError { get; set; }
		public string ErrorMessage { get; set; }
		public string SuccessMessage { get; set; }
	}
}

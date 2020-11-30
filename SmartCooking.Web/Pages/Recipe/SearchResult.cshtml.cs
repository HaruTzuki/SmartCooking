using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartCooking.Web.Pages.Recipe
{
    public class SearchResultModel : PageModel
    {
        public void OnGet()
        {
        }

        public bool CalculateMethod()
		{
            return true;
		}
    }
}

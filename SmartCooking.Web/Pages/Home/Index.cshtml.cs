using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;

namespace SmartCooking.Web.Pages
{
	public class IndexModel : PageModel
    {
        private readonly IItemRepository itemRepository;
        private readonly ILogger<IndexModel> _logger;
         
        public IndexModel(IItemRepository itemRepository,  ILogger<IndexModel> logger)
        {
            this.itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["IsLogged"] = HttpContext.Session.GetInt32("ISLOGIN");
            ViewData["Username"] = HttpContext.Session.GetString("USERNAME");
            ViewData["IsAdmin"] = HttpContext.Session.GetString("ISADMIN");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
		{
            // var list = Request.Form["ingrdientsSearchForm"];

            List<Item> items = new List<Item>();
            var list = new List<string>()
            {
                "Πιπέρι", // Πιπέρι
                "Κοτόπουλο", // Κοτόπουλο
                "Chilli" // Chilli
            };

            var text = "Πιπέρι&Κοτόπουλο&Chilli";


            return RedirectToPage("Recipe/SearchResult", new { query = text });
		}
    }
}

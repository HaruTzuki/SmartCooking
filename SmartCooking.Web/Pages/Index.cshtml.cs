using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Infastructure.Security;

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

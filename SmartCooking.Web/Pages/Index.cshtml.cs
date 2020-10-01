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
            //Item item = new Item()
            //{
            //    Name = "Πιπέρι"
            //};

            //itemRepository.InsertItem(item);

            var _item = await itemRepository.GetItem(1);

            return Page();
        }
    }
}

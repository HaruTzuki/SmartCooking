using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartCooking.Infastructure.Products
{
    public class ItemCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

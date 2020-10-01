using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCooking.Infastructure.Products
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ItemCategoryId { get; set; }
    }
}

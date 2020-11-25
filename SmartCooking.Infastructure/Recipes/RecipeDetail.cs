using SmartCooking.Infastructure.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartCooking.Infastructure.Recipes
{
    public class RecipeDetail
    {
        public int Id { get; set; }
        public int? ItemId { get; set; }
        [NotMapped]
        public Item Item { get; set; }
        public float Quantity { get; set; }
        public int? UnitId { get; set; }
        [NotMapped]
        public Unit Unit { get; set; }
        public int? RecipeHeaderId { get; set; }
    }
}



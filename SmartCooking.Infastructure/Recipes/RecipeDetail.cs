using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCooking.Infastructure.Recipes
{
    public class RecipeDetail
    {
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public float Quantity { get; set; }
        public int? UnitId { get; set; }
        public int? RecipeHeaderId { get; set; }
    }
}




using SmartCooking.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCooking.Infastructure.Recipes
{
    public class RecipeHeader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Tags { get; set; }
        public RecipeType RecipeType { get; set; }
    }
}

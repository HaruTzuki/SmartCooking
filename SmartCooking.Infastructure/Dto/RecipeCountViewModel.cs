using SmartCooking.Common.Structs;
using SmartCooking.Infastructure.Recipes;
using System.Collections.Generic;

namespace SmartCooking.Infastructure.Dto
{
	public class RecipeCountViewModel
	{
		public IEnumerable<RecipeHeader> RecipeHeaders { get; set; }
		public IEnumerable<RecipeHeaderCount> RecipeHeaderCounts { get; set; }
	}
}
using SmartCooking.Infastructure.Recipes;
using System.Collections.Generic;

namespace SmartCooking.Infastructure.Products
{
	/// <summary>
	/// Μοντέλο για Unit στη βάση δεδομένων
	/// </summary>
	public class Unit
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<RecipeDetail> RecipeDetails { get; set; }
	}
}
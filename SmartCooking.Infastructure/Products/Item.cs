using SmartCooking.Infastructure.Recipes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartCooking.Infastructure.Products
{
	/// <summary>
	/// Μοντελό για Item στη βάση δεδομένων
	/// </summary>
	public class Item
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public int? ItemCategoryId { get; set; }
		public ItemCategory ItemCategory { get; set; }
		public List<RecipeDetail> RecipeDetails { get; set; }
	}
}
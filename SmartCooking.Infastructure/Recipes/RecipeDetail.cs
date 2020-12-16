using SmartCooking.Infastructure.Products;

namespace SmartCooking.Infastructure.Recipes
{
	/// <summary>
	/// Μοντέλο για Recipe Detail στη βάση δεδομένων
	/// </summary>
	public class RecipeDetail
	{
		public int Id { get; set; }
		public int? ItemId { get; set; }

		public Item Item { get; set; }

		public float Quantity { get; set; }
		public int? UnitId { get; set; }

		public Unit Unit { get; set; }

		public int? RecipeHeaderId { get; set; }
	}
}
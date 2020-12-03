using SmartCooking.Common.Enumeration;

namespace SmartCooking.Infastructure.Recipes
{
	/// <summary>
	/// Μοντέλο για Recipe Header στη βάση δεδομένων
	/// </summary>
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
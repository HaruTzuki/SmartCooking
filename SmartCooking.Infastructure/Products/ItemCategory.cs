using System.ComponentModel.DataAnnotations;

namespace SmartCooking.Infastructure.Products
{
	/// <summary>
	/// Μοντέλο για Item Category στη βάση δεδομένων
	/// </summary>
	public class ItemCategory
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
using System.ComponentModel.DataAnnotations;

namespace SmartCooking.Infastructure.Products
{
	public class ItemCategory
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
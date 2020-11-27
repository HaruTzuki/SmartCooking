using System.ComponentModel.DataAnnotations;

namespace SmartCooking.Infastructure.Products
{
	public class Item
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public int? ItemCategoryId { get; set; }
	}
}
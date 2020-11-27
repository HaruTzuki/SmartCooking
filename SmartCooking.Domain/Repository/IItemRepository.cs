using SmartCooking.Infastructure.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Repository
{
	public interface IItemRepository
	{
		Task<Item> GetItem(int Id);

		Task<Item> GetItem(string name);

		Task<IEnumerable<Item>> GetItems();

		Task<bool> InsertItem(Item item);

		Task<bool> UpdateItem(Item item);

		Task<bool> DeleteItem(Item item);
	}
}
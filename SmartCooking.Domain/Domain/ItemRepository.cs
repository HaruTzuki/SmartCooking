using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{
	/// <summary>
	/// Κλάση που είναι υπεύθυνση για τις κλήσεις στη βάση που αφορούν τα Items.
	/// </summary>
	public class ItemRepository : IItemRepository
	{
		private readonly MyDbContext context;

		public ItemRepository(MyDbContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Διαγραφή Object
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public async Task<bool> DeleteItem(Item item)
		{
			context.SC_Item.Remove(item);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Επιστροφή ενώς Object
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task<Item> GetItem(int Id)
		{
			return await context.SC_Item.FirstOrDefaultAsync(x => x.Id == Id);
		}

		/// <summary>
		/// Επιστροφή ενώς Object
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public async Task<Item> GetItem(string name)
		{
			return await context.SC_Item.FirstOrDefaultAsync(x => x.Name == name);
		}

		/// <summary>
		/// Επιστροφή λίστα με Objects
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Item>> GetItems()
		{
			return await context.SC_Item.ToListAsync();
		}

		/// <summary>
		/// Προσθήκη Object
		/// </summary>
		/// <param name="itemCategory"></param>
		/// <returns></returns>
		public async Task<bool> InsertItem(Item item)
		{
			context.SC_Item.Add(item);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Ενημέρωση Object
		/// </summary>
		/// <param name="itemCategory"></param>
		/// <returns></returns>
		public async Task<bool> UpdateItem(Item item)
		{
			context.SC_Item.Update(item);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}
	}
}
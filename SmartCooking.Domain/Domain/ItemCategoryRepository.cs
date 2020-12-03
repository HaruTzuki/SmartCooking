using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{
	/// <summary>
	/// Κλάση που είναι υπεύθυνση για τις κλήσεις στη βάση που αφορούν τα Items Category.
	/// </summary>
	public class ItemCategoryRepository : IItemCategoryRepository
	{
		private readonly MyDbContext context;

		public ItemCategoryRepository(MyDbContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Διαγραφή Object
		/// </summary>
		/// <param name="itemCategory"></param>
		/// <returns></returns>
		public async Task<bool> DeleteItemCategory(ItemCategory itemCategory)
		{
			context.SC_ItemCategory.Remove(itemCategory);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Επιστροφή λίστα με Objects
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<ItemCategory>> GetItemCategories()
		{
			return await context.SC_ItemCategory.ToListAsync();
		}

		/// <summary>
		/// Επιστροφή ενώς Object
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task<ItemCategory> GetItemCategory(int Id)
		{
			return await context.SC_ItemCategory.FirstOrDefaultAsync(x => x.Id == Id);
		}

		/// <summary>
		/// Προσθήκη Object
		/// </summary>
		/// <param name="itemCategory"></param>
		/// <returns></returns>
		public async Task<bool> InsertItemCategory(ItemCategory itemCategory)
		{
			context.SC_ItemCategory.Add(itemCategory);

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
		public async Task<bool> UpdateItemCategory(ItemCategory itemCategory)
		{
			context.SC_ItemCategory.Update(itemCategory);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}
	}
}
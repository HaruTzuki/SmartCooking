using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{
	/// <summary>
	/// Κλάση που είναι υπεύθυνση για τις κλήσεις στη βάση που αφορούν τα Unit.
	/// </summary>
	public class UnitRepository : IUnitRepository
	{
		private readonly MyDbContext context;

		public UnitRepository(MyDbContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Διαγραφή Object
		/// </summary>
		/// <param name="unit"></param>
		/// <returns></returns>
		public async Task<bool> DeleteUnit(Unit unit)
		{
			context.SC_Unit.Remove(unit);

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
		public async Task<Unit> GetUnit(int Id)
		{
			return await context.SC_Unit.FirstOrDefaultAsync(x => x.Id == Id);
		}

		/// <summary>
		/// Επιστροφή λίστα ενώς Object
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Unit>> GetUnits()
		{
			return await context.SC_Unit.ToListAsync();
		}

		/// <summary>
		/// Προσθήκη Object
		/// </summary>
		/// <param name="unit"></param>
		/// <returns></returns>
		public async Task<bool> InsertUnit(Unit unit)
		{
			context.SC_Unit.Add(unit);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Ενημέρωση Object
		/// </summary>
		/// <param name="unit"></param>
		/// <returns></returns>
		public async Task<bool> UpdateUnit(Unit unit)
		{
			context.SC_Unit.Update(unit);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}
	}
}
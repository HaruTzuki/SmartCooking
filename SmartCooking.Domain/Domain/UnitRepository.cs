using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{
	public class UnitRepository : IUnitRepository
	{
		private readonly MyDbContext context;

		public UnitRepository(MyDbContext context)
		{
			this.context = context;
		}

		public async Task<bool> DeleteUnit(Unit unit)
		{
			context.SC_Unit.Remove(unit);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		public async Task<Unit> GetUnit(int Id)
		{
			return await context.SC_Unit.FirstOrDefaultAsync(x => x.Id == Id);
		}

		public async Task<IEnumerable<Unit>> GetUnits()
		{
			return await context.SC_Unit.ToListAsync();
		}

		public async Task<bool> InsertUnit(Unit unit)
		{
			context.SC_Unit.Add(unit);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

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

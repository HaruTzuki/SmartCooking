using SmartCooking.Infastructure.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Repository
{
	public interface IUnitRepository
	{
		Task<Unit> GetUnit(int Id);
		Task<IEnumerable<Unit>> GetUnits();
		Task<bool> InsertUnit(Unit unit);
		Task<bool> UpdateUnit(Unit unit);
		Task<bool> DeleteUnit(Unit unit);
	}
}

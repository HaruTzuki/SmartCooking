using SmartCooking.Infastructure.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCooking.Data.Repository
{
    public interface IUnitRepository
    {
        Unit GetUnit(int Id);
        List<Unit> GetUnits();
        bool InsertUnit(Unit unit);
        bool UpdateUnit(Unit unit);
        bool DeleteUnit(Unit unit);
    }
}

using SmartCooking.Infastructure.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCooking.Data.Repository
{
    public interface IItemRepository
    {
        Item GetItem(int Id);
        List<Item> GetItems();
        bool InsertItem(Item item);
        bool UpdateItem(Item item);
        bool DeleteItem(Item item);
    }
}

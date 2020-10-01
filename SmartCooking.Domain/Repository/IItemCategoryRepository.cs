using SmartCooking.Infastructure.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCooking.Data.Repository
{
    public interface IItemCategoryRepository
    {
        ItemCategory GetItemCategory(int Id);
        List<ItemCategory> GetItemCategories();
        bool InsertItemCategory(ItemCategory itemCategory);
        bool UpdateItemCategory(ItemCategory itemCategory);
        bool DeleteItemCategory(ItemCategory itemCategory);
    }
}

using SmartCooking.Infastructure.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Repository
{
    public interface IItemCategoryRepository
    {
        Task<ItemCategory> GetItemCategory(int Id);
        Task<List<ItemCategory>> GetItemCategories();
        Task<bool> InsertItemCategory(ItemCategory itemCategory);
        Task<bool> UpdateItemCategory(ItemCategory itemCategory);
        Task<bool> DeleteItemCategory(ItemCategory itemCategory);
    }
}

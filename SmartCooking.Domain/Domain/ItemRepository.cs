using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{
    public class ItemRepository : IItemRepository
    {
        private readonly MyDbContext context;
        public ItemRepository(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteItem(Item item)
        {
            context.SC_Item.Remove(item);

            if (await context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public async Task<Item> GetItem(int Id)
        {
            return await context.SC_Item.FirstOrDefaultAsync(x => x.Id == Id);
        }

		public async Task<Item> GetItem(string name)
		{
            return await context.SC_Item.FirstOrDefaultAsync(x => x.Name == name); 
		}

		public async Task<List<Item>> GetItems()
        {
            return await context.SC_Item.ToListAsync();
        }

        public async Task<bool> InsertItem(Item item)
        {
            context.SC_Item.Add(item);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateItem(Item item)
        {
            context.SC_Item.Update(item);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }
    }
}

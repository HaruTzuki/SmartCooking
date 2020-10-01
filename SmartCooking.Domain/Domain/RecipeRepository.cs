using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Recipes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly MyDbContext context;

        public RecipeRepository(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteRecipeDetail(RecipeDetail recipeDetail)
        {
            context.SC_RecipeDetail.Remove(recipeDetail);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteRecipeHeader(RecipeHeader recipeHeader)
        {
            context.SC_RecipeHeader.Remove(recipeHeader);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<RecipeDetail> GetRecipeDetail(int Id)
        {
            return await context.SC_RecipeDetail.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<RecipeDetail>> GetRecipeDetails()
        {
            return await context.SC_RecipeDetail.ToListAsync();
        }

        public async Task<List<RecipeDetail>> GetRecipeDetails(int RecipeHeaderId)
        {
            return await context.SC_RecipeDetail.Where(x=>x.RecipeHeaderId == RecipeHeaderId).ToListAsync();
        }

        public async Task<RecipeHeader> GetRecipeHeader(int Id)
        {
            return await context.SC_RecipeHeader.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<RecipeHeader>> GetRecipeHeaders()
        {
            return await context.SC_RecipeHeader.ToListAsync();
        }

        public async Task<bool> InsertRecipeDetail(RecipeDetail recipeDetail)
        {
            context.SC_RecipeDetail.Add(recipeDetail);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> InsertRecipeHeader(RecipeHeader recipeHeader)
        {
            context.SC_RecipeHeader.Add(recipeHeader);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateRecipeDetail(RecipeDetail recipeDetail)
        {
            context.SC_RecipeDetail.Update(recipeDetail);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateRecipeHeader(RecipeHeader recipeHeader)
        {
            context.SC_RecipeHeader.Update(recipeHeader);

            if(await context.SaveChangesAsync() <= 0)
            {
                return false;
            }

            return true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{
	public class RecipeImageRepository : IRecipeImageRepository
	{
		private readonly MyDbContext context;

		public RecipeImageRepository(MyDbContext context)
		{
			this.context = context;
		}

		public async Task<bool> DeleteRecipeImage(RecipeImage recipeImage)
		{
			context.SC_RecipeImage.Remove(recipeImage);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		public async Task<RecipeImage> GetRecipeImage(int id)
		{
			return await context.SC_RecipeImage.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<IEnumerable<RecipeImage>> GetRecipeImages()
		{
			return await context.SC_RecipeImage.ToListAsync();
		}

		public async Task<IEnumerable<RecipeImage>> GetRecipeImages(int recipeId)
		{
			return await context.SC_RecipeImage.Where(x => x.RecipeId == recipeId).ToListAsync();
		}

		public async Task<bool> InsertRecipeImage(RecipeImage recipeImage)
		{
			context.SC_RecipeImage.Add(recipeImage);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> UpdateProfileImage(int recipeId, int imageId)
		{
			var dbImages = context.SC_RecipeImage.Where(x => x.RecipeId == recipeId);

			foreach (var dbImage in dbImages)
			{
				if (dbImage.Id == imageId)
					dbImage.ProfileImage = true;
				else
					dbImage.ProfileImage = false;
			}

			context.SC_RecipeImage.UpdateRange(dbImages);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> UpdateRecipeImage(RecipeImage recipeImage)
		{
			context.SC_RecipeImage.Update(recipeImage);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}


	}
}

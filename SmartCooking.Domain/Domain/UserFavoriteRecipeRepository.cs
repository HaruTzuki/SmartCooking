using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Recipes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{
	public class UserFavoriteRecipeRepository : IUserFavoriteRecipeRepository
	{
		private readonly MyDbContext context;

		public UserFavoriteRecipeRepository(MyDbContext context)
		{
			this.context = context;
		}

		public async Task<bool> DeleteUserFavoriteRecipe(UserFavoriteRecipe userFavoriteRecipe)
		{
			context.SC_UserFavoriteRecipe.Remove(userFavoriteRecipe);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		public async Task<UserFavoriteRecipe> GetUserFavoriteRecipe(int id)
		{
			return await context.SC_UserFavoriteRecipe
				.Include(u => u.User)
				.Include(r => r.RecipeHeader)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<IEnumerable<UserFavoriteRecipe>> GetUserFavoriteRecipes()
		{
			return await context.SC_UserFavoriteRecipe
				.Include(u => u.User)
				.Include(r => r.RecipeHeader)
				.ToListAsync();
		}

		public async Task<IEnumerable<UserFavoriteRecipe>> GetUserFavoriteRecipes(int userId)
		{
			return await context.SC_UserFavoriteRecipe
				.Include(u => u.User)
				.Include(r => r.RecipeHeader)
				.Where(x => x.UserId == userId)
				.ToListAsync();
		}

		public async Task<bool> InsertUserFavoriteRecipe(UserFavoriteRecipe userFavoriteRecipe)
		{
			context.SC_UserFavoriteRecipe.Add(userFavoriteRecipe);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> UpdateUserFavoriteRecipe(UserFavoriteRecipe userFavoriteRecipe)
		{
			context.SC_UserFavoriteRecipe.Update(userFavoriteRecipe);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}
	}
}
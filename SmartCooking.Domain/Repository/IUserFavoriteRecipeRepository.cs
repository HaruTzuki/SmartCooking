using SmartCooking.Infastructure.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCooking.Data.Repository
{
	public interface IUserFavoriteRecipeRepository
	{
		Task<UserFavoriteRecipe> GetUserFavoriteRecipe(int id);
		Task<IEnumerable<UserFavoriteRecipe>> GetUserFavoriteRecipes();
		Task<IEnumerable<UserFavoriteRecipe>> GetUserFavoriteRecipes(int userId);
		Task<bool> InsertUserFavoriteRecipe(UserFavoriteRecipe userFavoriteRecipe);
		Task<bool> UpdateUserFavoriteRecipe(UserFavoriteRecipe userFavoriteRecipe);
		Task<bool> DeleteUserFavoriteRecipe(UserFavoriteRecipe userFavoriteRecipe);
	}
}

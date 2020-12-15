using SmartCooking.Infastructure.Recipes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Repository
{
	public interface IRecipeImageRepository
	{
		Task<RecipeImage> GetRecipeImage(int id);

		Task<IEnumerable<RecipeImage>> GetRecipeImages();
		Task<IEnumerable<RecipeImage>> GetRecipeImages(int recipeId);

		Task<bool> InsertRecipeImage(RecipeImage recipeImage);

		Task<bool> UpdateRecipeImage(RecipeImage recipeImage);

		Task<bool> DeleteRecipeImage(RecipeImage recipeImage);

		Task<bool> UpdateProfileImage(int recipeId, int imageId);
	}
}
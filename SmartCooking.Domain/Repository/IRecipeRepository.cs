using SmartCooking.Infastructure.Recipes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCooking.Data.Repository
{
    public interface IRecipeRepository
    {
        Task<RecipeHeader> GetRecipeHeader(int Id);
        Task<RecipeDetail> GetRecipeDetail(int Id);
        Task<IEnumerable<RecipeHeader>> GetRecipeHeaders();
        Task<IEnumerable<RecipeDetail>> GetRecipeDetails();
        Task<IEnumerable<RecipeDetail>> GetRecipeDetails(int RecipeHeaderId);
        Task<bool> InsertRecipeHeader(RecipeHeader recipeHeader);
        Task<bool> InsertRecipeDetail(RecipeDetail recipeDetail);
        Task<bool> UpdateRecipeHeader(RecipeHeader recipeHeader);
        Task<bool> UpdateRecipeDetail(RecipeDetail recipeDetail);
        Task<bool> DeleteRecipeHeader(RecipeHeader recipeHeader);
        Task<bool> DeleteRecipeDetail(RecipeDetail recipeDetail);

    }
}

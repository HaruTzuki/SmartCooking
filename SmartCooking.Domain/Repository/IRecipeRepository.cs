using SmartCooking.Infastructure.Recipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCooking.Data.Repository
{
    public interface IRecipeRepository
    {
        RecipeHeader GetRecipeHeader(int Id);
        RecipeDetail GetRecipeDetail(int Id);
        List<RecipeHeader> GetRecipeHeaders();
        List<RecipeDetail> GetRecipeDetails();
        List<RecipeDetail> GetRecipeDetails(int RecipeHeaderId);
        bool InsertRecipeHeader(RecipeHeader recipeHeader);
        bool InsertRecipeDetail(RecipeDetail recipeDetail);
        bool UpdateRecipeHeader(RecipeHeader recipeHeader);
        bool UpdateRecipeDetail(RecipeDetail recipeDetail);
        bool DeleteRecipeHeader(RecipeHeader recipeHeader);
        bool DeleteRecipeDetail(RecipeDetail recipeDetail);

    }
}

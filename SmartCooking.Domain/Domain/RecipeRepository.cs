using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Recipes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCooking.Data.Domain
{
    public class RecipeRepository : IRecipeRepository
    {
        public bool DeleteRecipeDetail(RecipeDetail recipeDetail)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecipeHeader(RecipeHeader recipeHeader)
        {
            throw new NotImplementedException();
        }

        public RecipeDetail GetRecipeDetail(int Id)
        {
            throw new NotImplementedException();
        }

        public List<RecipeDetail> GetRecipeDetails()
        {
            throw new NotImplementedException();
        }

        public List<RecipeDetail> GetRecipeDetails(int RecipeHeaderId)
        {
            throw new NotImplementedException();
        }

        public RecipeHeader GetRecipeHeader(int Id)
        {
            throw new NotImplementedException();
        }

        public List<RecipeHeader> GetRecipeHeaders()
        {
            throw new NotImplementedException();
        }

        public bool InsertRecipeDetail(RecipeDetail recipeDetail)
        {
            throw new NotImplementedException();
        }

        public bool InsertRecipeHeader(RecipeHeader recipeHeader)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecipeDetail(RecipeDetail recipeDetail)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecipeHeader(RecipeHeader recipeHeader)
        {
            throw new NotImplementedException();
        }
    }
}

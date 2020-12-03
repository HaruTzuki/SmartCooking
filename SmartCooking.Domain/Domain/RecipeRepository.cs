using Microsoft.EntityFrameworkCore;
using SmartCooking.Data.Context;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Recipes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Data.Domain
{
	/// <summary>
	/// Κλάση που είναι υπεύθυνση για τις κλήσεις στη βάση που αφορούν τα Recipe.
	/// </summary>
	public class RecipeRepository : IRecipeRepository
	{
		private readonly MyDbContext context;

		public RecipeRepository(MyDbContext context)
		{
			this.context = context;
		}

		#region Recipe

		/// <summary>
		/// Διαγραφή Object
		/// </summary>
		/// <param name="recipeDetail"></param>
		/// <returns></returns>
		public async Task<bool> DeleteRecipeDetail(RecipeDetail recipeDetail)
		{
			context.SC_RecipeDetail.Remove(recipeDetail);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Διαγραφή του Object
		/// </summary>
		/// <param name="recipeHeader"></param>
		/// <returns></returns>
		public async Task<bool> DeleteRecipeHeader(RecipeHeader recipeHeader)
		{
			context.SC_RecipeHeader.Remove(recipeHeader);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Επιστροφή ενώς Object
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<RecipeDetail> GetRecipeDetail(int id)
		{
			return await context.SC_RecipeDetail.FirstOrDefaultAsync(x => x.Id == id);
		}

		/// <summary>
		/// Επιστροφή λίστα με Objects
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<RecipeDetail>> GetRecipeDetails()
		{
			return await context.SC_RecipeDetail.ToListAsync();
		}

		/// <summary>
		/// Επιστροφή λίστα με Objects
		/// </summary>
		/// <param name="recipeHeaderId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<RecipeDetail>> GetRecipeDetails(int recipeHeaderId)
		{
			List<RecipeDetail> details = await context.SC_RecipeDetail.Where(x => x.RecipeHeaderId == recipeHeaderId).ToListAsync();
			details.ToList().ForEach(x =>
			{
				x.Item = context.SC_Item.FirstOrDefaultAsync(item => item.Id == x.ItemId).Result;
				x.Unit = context.SC_Unit.FirstOrDefaultAsync(unit => unit.Id == x.UnitId).Result;
			});

			return details;
		}

		/// <summary>
		/// Επιστροφή ενώς Object
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<RecipeHeader> GetRecipeHeader(int id)
		{
			return await context.SC_RecipeHeader.FirstOrDefaultAsync(x => x.Id == id);
		}

		/// <summary>
		/// Επιστροφή λίστα με Objects.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<RecipeHeader>> GetRecipeHeaders()
		{
			return await context.SC_RecipeHeader.ToListAsync();
		}

		/// <summary>
		/// Προσθήκη Object
		/// </summary>
		/// <param name="recipeDetail"></param>
		/// <returns></returns>
		public async Task<bool> InsertRecipeDetail(RecipeDetail recipeDetail)
		{
			context.SC_RecipeDetail.Add(recipeDetail);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Προσθήκη Object
		/// </summary>
		/// <param name="recipeHeader"></param>
		/// <returns></returns>
		public async Task<bool> InsertRecipeHeader(RecipeHeader recipeHeader)
		{
			context.SC_RecipeHeader.Add(recipeHeader);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Ενημέρωση Object
		/// </summary>
		/// <param name="recipeDetail"></param>
		/// <returns></returns>
		public async Task<bool> UpdateRecipeDetail(RecipeDetail recipeDetail)
		{
			context.SC_RecipeDetail.Update(recipeDetail);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Ενημέρωση Object
		/// </summary>
		/// <param name="recipeHeader"></param>
		/// <returns></returns>
		public async Task<bool> UpdateRecipeHeader(RecipeHeader recipeHeader)
		{
			context.SC_RecipeHeader.Update(recipeHeader);

			if (await context.SaveChangesAsync() <= 0)
			{
				return false;
			}

			return true;
		}

		#endregion Recipe
	}
}
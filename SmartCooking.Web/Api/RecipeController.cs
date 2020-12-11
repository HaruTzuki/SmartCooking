using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Products;
using SmartCooking.Infastructure.Recipes;
using SmartCooking.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Threading.Tasks;

namespace SmartCooking.Web.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipeController : CustomController
	{
		private readonly IRecipeRepository recipeRepository;
		private readonly IItemRepository itemRepository;
		private readonly IUnitRepository unitRepository;

		public RecipeController(IRecipeRepository recipeRepository, IItemRepository itemRepository, IUnitRepository unitRepository)
		{
			this.recipeRepository = recipeRepository;
			this.itemRepository = itemRepository;
			this.unitRepository = unitRepository;
		}

		[HttpPost("DeleteDetailItem")]
		public async Task<JsonResult> DeleteDetailItem([FromBody] int recipeDetailId)
		{
			var dbRecipeDetail = await recipeRepository.GetRecipeDetail(recipeDetailId);

			if (dbRecipeDetail is null)
			{
				HasError = true;
				ErrorMessage = "Το είδος που ζητήσατε να γίνει διαγραφή δεν υπάρχει στη λίστα.";
				return new JsonResult(new { result = false, message = ErrorMessage });
			}

			if (!await recipeRepository.DeleteRecipeDetail(dbRecipeDetail))
			{
				HasError = true;
				ErrorMessage = "Κάτι πήγε στραβά και δεν μπορέσαμε να διαγράψουμε την εγγραφή σας.";
				return new JsonResult(new { result = false, message = ErrorMessage });
			}

			HasError = false;
			SuccessMessage = "Η διαγραφή του είδους έγινε με επιτυχία.";
			return new JsonResult(new { result = true, message = SuccessMessage });
		}

		[HttpPost("EditRecipeItem")]
		public async Task<JsonResult> EditRecipeItem([FromBody] RecipeItemDetail sender)
		{
			var itemName = sender.itemName;
			var unitName = sender.unitName;
			var qty = sender.qty;
			var recipeDetailId = sender.recipeDetailId;

			Item itemObj = new Item();
			Unit unitObj = new Unit();

			IEnumerable<Item> itemList = await itemRepository.GetItems();
			IEnumerable<Unit> unitList = await unitRepository.GetUnits();

			if (!itemList.Any(x => x.Name.ToLower() == itemName.ToLower().Trim()))
			{
				itemObj.Name = itemName.Trim();
				await itemRepository.InsertItem(itemObj);
			}
			else
			{
				itemObj = itemList.First(x => x.Name.ToLower() == itemName.ToLower().Trim());
			}

			if (!unitList.Any(x => x.Name.ToLower() == unitName.ToLower().Trim()))
			{
				unitObj.Name = unitName.Trim();
				await unitRepository.InsertUnit(unitObj);
			}
			else
			{
				unitObj = unitList.First(x => x.Name.ToLower() == unitName.ToLower().Trim());
			}

			RecipeDetail recipeDetail = await recipeRepository.GetRecipeDetail(recipeDetailId);

			if (recipeDetail == null)
			{
				HasError = true;
				ErrorMessage = "Δεν υπάρχει το είδος στη συνταγή.";
				return new JsonResult(new { result = true, message = ErrorMessage });
			}

			recipeDetail.Item = itemObj;
			recipeDetail.ItemId = itemObj.Id;
			recipeDetail.Unit = unitObj;
			recipeDetail.UnitId = unitObj.Id;
			recipeDetail.Quantity = qty;

			if (!await recipeRepository.UpdateRecipeDetail(recipeDetail))
			{
				HasError = true;
				ErrorMessage = "Δεν μπορέσαμε να ενημερώσουμε την εγγραφή.";
				return new JsonResult(new { result = true, message = ErrorMessage });
			}

			HasError = false;
			SuccessMessage = "Η ενημέρωση έγινε με επιτυχία.";
			return new JsonResult(new { result = true, message = SuccessMessage });
		}
		
	}

	public record RecipeItemDetail(string itemName, string unitName, float qty, int recipeDetailId);

}
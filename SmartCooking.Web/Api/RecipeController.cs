using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SmartCooking.Common.Extensions;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Dto;
using SmartCooking.Infastructure.Products;
using SmartCooking.Infastructure.Recipes;
using SmartCooking.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		private readonly IRecipeImageRepository recipeImageRepository;

		public RecipeController(IRecipeRepository recipeRepository, IItemRepository itemRepository, IUnitRepository unitRepository, IRecipeImageRepository recipeImageRepository)
		{
			this.recipeRepository = recipeRepository;
			this.itemRepository = itemRepository;
			this.unitRepository = unitRepository;
			this.recipeImageRepository = recipeImageRepository;
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

		[HttpPost("UploadRecipeImage")]
		public async Task<JsonResult> UploadRecipeImage()
		{
			var files = Request.Form.Files;
			int recipeId = Request.Form.FirstOrDefault(x => x.Key == "recipeId").Value.ToString().ToInt();

			foreach (var file in files)
			{
				using var ms = new MemoryStream();
				file.CopyTo(ms);
				var fileBytes = ms.ToArray();
				var recipeImage = new RecipeImage()
				{
					RecipeId = recipeId,
					FileName = file.FileName,
					FileSize = file.Length,
					ContentType = file.ContentType,
					ContentValue = Convert.ToBase64String(fileBytes)
				};

				if (!await recipeImageRepository.InsertRecipeImage(recipeImage))
				{
					return new JsonResult(new { result = false, message = "Ζητάμε συγνώμη δεν μπορέσαμε να αποθηκεύσουμε τις φωτογραφίες σας." });
				}
			}

			return new JsonResult(new { result = true });
		}

		[HttpPost("UpdateImageProfile")]
		public async Task<JsonResult> UpdateImageProfile([FromBody] UpdateImageProfileDto obj)
		{
			if (!await recipeImageRepository.UpdateProfileImage(obj.RecipeId, obj.ImageId))
			{
				HasError = true;
				ErrorMessage = "Κάτι πήγε στραβά και δεν μπορέσαμε να ενημερώσουμε την φωτογραφία προφίλ.";
				return new JsonResult(new { result = false, message = ErrorMessage });
			}

			return new JsonResult(new { result = true });
		}

		[HttpPost("DeleteRecipeImage")]
		public async Task<JsonResult> DeleteRecipeImage([FromBody] int imgId)
		{
			var dbImg = await recipeImageRepository.GetRecipeImage(imgId);
			
			if(dbImg is null)
			{
				HasError = true;
				ErrorMessage = "Δεν υπάρχει η φωτογραφία που ζητήσατε";
				return new JsonResult(new { result = true, message = ErrorMessage });
			}

			if(!await recipeImageRepository.DeleteRecipeImage(dbImg))
			{
				HasError = true;
				ErrorMessage = "Κάτι πήγε στραβά και δεν μπορέσαμε να διαγράψουμε την φωτογραφία προφίλ.";
				return new JsonResult(new { result = true, message = ErrorMessage });
			}

			HasError = false;

			return new JsonResult(new { result = true });
		}
	}

	public record RecipeItemDetail(string itemName, string unitName, float qty, int recipeDetailId);
}
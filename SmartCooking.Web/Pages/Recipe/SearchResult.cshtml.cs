using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartCooking.Common.Structs;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Dto;
using SmartCooking.Infastructure.Products;
using SmartCooking.Infastructure.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCooking.Web.Pages.Recipe
{
	public class SearchResultModel : PageModel
	{
		private readonly IItemRepository itemRepository;
		private readonly IRecipeRepository recipeRepository;
		private readonly IRecipeImageRepository recipeImageRepository;

		public RecipeCountViewModel RecipeCountViewModel { get; set; }
		public List<RecipeImage> RecipeImages { get; set; } 
		[BindProperty(SupportsGet = true)] public string IngredientsText { get; set; }


		public SearchResultModel(IItemRepository itemRepository, IRecipeRepository recipeRepository, IRecipeImageRepository recipeImageRepository)
		{
			this.itemRepository = itemRepository;
			this.recipeRepository = recipeRepository;
			this.recipeImageRepository = recipeImageRepository;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			List<string> ingredients = IngredientsText.Split("-", StringSplitOptions.RemoveEmptyEntries).ToList();

			var dbItems = await itemRepository.GetItems();

			// Διαγραφή όποιων υλικών δεν υπάρχουν
			for (int i = ingredients.Count - 1; i >= 0; i--)
			{
				if (!dbItems.Any(itm => itm.Name == ingredients[i]))
				{
					ingredients.Remove(ingredients[i]);
				}
			}

			List<Item> items = new List<Item>();
			foreach (var ingredient in ingredients)
			{
				items.Add(await itemRepository.GetItem(ingredient));
			}

			List<RecipeHeader> recipeHeaders = new List<RecipeHeader>();
			var dbRecipeDetails = await recipeRepository.GetRecipeDetails();

			foreach (var item in items)
			{
				var tempRecipeDetails = dbRecipeDetails.Where(x => x.ItemId == item.Id);

				foreach (var tempRecipeDetail in tempRecipeDetails)
				{
					if (tempRecipeDetail.RecipeHeaderId.HasValue)
					{
						recipeHeaders.Add(await recipeRepository.GetRecipeHeader(tempRecipeDetail.RecipeHeaderId.Value));
					}
				}
			}

			var recipeCountViewModel = new RecipeCountViewModel();

			recipeCountViewModel.RecipeHeaders = recipeHeaders.Distinct();
			recipeCountViewModel.RecipeHeaderCounts = recipeHeaders
													.GroupBy(g => g.Id)
													.Select(x => new RecipeHeaderCount() { Key = x.Key, Count = x.Count() });

			RecipeCountViewModel = recipeCountViewModel;

			foreach(var recipeHeader in RecipeCountViewModel.RecipeHeaders)
			{
				var img = (await recipeImageRepository.GetRecipeImages(recipeHeader.Id))?.FirstOrDefault(x => x.ProfileImage);
				if (img != null)
				{
					if(RecipeImages is null)
					{
						RecipeImages = new List<RecipeImage>();
					}

					RecipeImages.Add(img);
				}

			}

			return Page();
		}
	}
}
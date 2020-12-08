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

		public RecipeCountViewModel RecipeCountViewModel { get; set; }

		public SearchResultModel(IItemRepository itemRepository, IRecipeRepository recipeRepository)
		{
			this.itemRepository = itemRepository;
			this.recipeRepository = recipeRepository;
		}

		public async Task<IActionResult> OnGetAsync(string ingredientsText)
		{
			List<string> ingredients = ingredientsText.Split("&", StringSplitOptions.RemoveEmptyEntries).ToList();

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

			return Page();
		}
	}
}
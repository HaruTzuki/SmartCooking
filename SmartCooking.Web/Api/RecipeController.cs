using Microsoft.AspNetCore.Mvc;
using SmartCooking.Data.Repository;

namespace SmartCooking.Web.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipeController : Controller
	{
		private readonly IRecipeRepository recipeRepository;
		private readonly IItemRepository itemRepository;

		public RecipeController(IRecipeRepository recipeRepository, IItemRepository itemRepository)
		{
			this.recipeRepository = recipeRepository;
			this.itemRepository = itemRepository;
		}
	}
}
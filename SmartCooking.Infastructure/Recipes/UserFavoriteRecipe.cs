using SmartCooking.Infastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCooking.Infastructure.Recipes
{
	public class UserFavoriteRecipe
	{
		public int Id { get; set; }
		public int? RecipeHeaderId {get;set;}
		public RecipeHeader RecipeHeader { get; set; }
		public int? UserId { get; set; }
		public User User { get; set; }
	}
}

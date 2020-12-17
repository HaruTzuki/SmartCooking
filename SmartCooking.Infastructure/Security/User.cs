using SmartCooking.Infastructure.Recipes;
using System.Collections.Generic;

namespace SmartCooking.Infastructure.Security
{
	/// <summary>
	/// Μοντέλο για User στη βάση δεδομένων
	/// </summary>
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string FamilyName { get; set; }
		public bool IsAdmin { get; set; }
		public List<UserFavoriteRecipe> UserFavoriteRecipe { get; set; }
	}
}
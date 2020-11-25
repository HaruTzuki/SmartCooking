using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartCooking.Infastructure.Security
{
	public class User
	{
		public int Id { get; set; }
		[Required, DisplayName("Όνομα Χρήστη")]
		public string Username { get; set; }
		[DisplayName("Κωδικός Πρόσβασης")]
		public string Password { get; set; }
		[Required, DisplayName("Ηλ. Ταχυδρομείο")]
		public string Email { get; set; }
		[DisplayName("Ονοματεπώνυμο")]
		public string FamilyName { get; set; }
		public bool IsAdmin { get; set; }
	}
}

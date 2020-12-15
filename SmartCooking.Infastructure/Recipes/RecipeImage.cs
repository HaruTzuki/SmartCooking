using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCooking.Infastructure.Recipes
{
	public class RecipeImage
	{
		public int Id { get; set; }
		public int? RecipeId { get; set; }
		public string FileName { get; set; }
		public long FileSize { get; set; }
		public string ContentType { get; set; }
		public string ContentValue { get; set; }
		public bool ProfileImage { get; set; }
	}
}

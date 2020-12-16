using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartCooking.Infastructure.Recipes;

namespace SmartCooking.Web.Helpers
{
	static public class HtmlHelpersExtensions
	{
		static public HtmlString ConvertImageToHtml(this IHtmlHelper htmlHelper, RecipeImage image)
		{
			if (image is null)
			{
				return new HtmlString("");
			}

			return new HtmlString($"data:{image.ContentType};charset=utf-8;base64,{image.ContentValue}");
		}
	}
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace SmartCooking.Common.Extensions
{
	static public class LinqExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
		{
			foreach (var item in sequence) action(item);
		}


		public static void ForEach<T>(this IEnumerable<T> sequence, Func<T, IActionResult> action)
		{
			foreach (var item in sequence)
			{
				action(item);
			}
		}

	}
}
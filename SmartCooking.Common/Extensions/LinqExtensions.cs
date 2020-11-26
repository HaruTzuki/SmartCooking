using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace SmartCooking.Common.Extensions
{
	public static class LinqExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
		{
			foreach (T item in sequence)
			{
				action(item);
			}
		}


		public static void ForEach<T>(this IEnumerable<T> sequence, Func<T, IActionResult> action)
		{
			foreach (T item in sequence)
			{
				action(item);
			}
		}

	}
}
using System;
using System.Globalization;

namespace SmartCooking.Common.Extensions
{
	public static class StringExtensions
	{
		#region ToInt Region
		public static int ToInt(this string s, int defaultValue = default(int))
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return defaultValue;
			}

			if (int.TryParse(s, out int result))
			{
				return result;
			}

			return defaultValue;
		}
		#endregion

		#region ToLong Region
		public static long ToLong(this string s, long defaultValue = default(long))
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return defaultValue;
			}

			if (long.TryParse(s, out long result))
			{
				return result;
			}

			return defaultValue;
		}
		#endregion

		#region ToFloat Region
		public static float ToFloat(this string s, float defaultValue = default(float))
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return defaultValue;
			}

			if (float.TryParse(s, out float result))
			{
				return result;
			}

			return defaultValue;
		}

		public static float ToFloat(this string s, NumberStyles numberStyles, IFormatProvider formatProvider, float defaultValue = default(float))
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return defaultValue;
			}

			if (float.TryParse(s, numberStyles, formatProvider, out float result))
			{
				return result;
			}

			return defaultValue;
		}
		#endregion

		#region ToDouble Region
		public static double ToDouble(this string s, double defaultValue = default(double))
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return defaultValue;
			}

			if (double.TryParse(s, out double result))
			{
				return result;
			}

			return defaultValue;
		}

		public static double ToDouble(this string s, NumberStyles numberStyles, IFormatProvider formatProvider, double defaultValue = default(double))
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return defaultValue;
			}

			if (double.TryParse(s, numberStyles, formatProvider, out double result))
			{
				return result;
			}

			return defaultValue;
		}
		#endregion

		#region ToDecimal Region
		public static decimal ToDecimal(this string s, decimal defaultValue = default(decimal))
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return defaultValue;
			}

			if (decimal.TryParse(s, out decimal result))
			{
				return result;
			}

			return defaultValue;
		}

		public static decimal ToDecimal(this string s, NumberStyles numberStyles, IFormatProvider formatProvider, decimal defaultValue = default(decimal))
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return defaultValue;
			}

			if (decimal.TryParse(s, numberStyles, formatProvider, out decimal result))
			{
				return result;
			}

			return defaultValue;
		}
		#endregion
	}

}
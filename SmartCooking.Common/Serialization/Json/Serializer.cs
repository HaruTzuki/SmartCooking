using Newtonsoft.Json;

#nullable enable

namespace SmartCooking.Common.Serialization.Json
{
	/// <summary>
	/// Κλαση κωδικοποίησης και αποκωδικοποίησης σε και από JSON κείμενο.
	/// </summary>
	public static class Serializer
	{
		/// <summary>
		/// Μετατρέπει ένα Object οποιοδήποτε τύπου σε JSON κείμενο
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static string JsonSerializer(object? o)
		{
			if (o is null)
			{
				return "{}";
			}

			return JsonConvert.SerializeObject(o, Formatting.Indented);
		}

		/// <summary>
		/// Μετατρέπει ένα κείμενο σε τύπο δεδομένων που του ορίζουμε.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <returns></returns>
		public static T JsonDeserializer<T>(string? s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return default(T);
			}

			return JsonConvert.DeserializeObject<T>(s);
		}
	}
}
using Newtonsoft.Json;

#nullable enable
namespace SmartCooking.Common.Serialization.Json
{
	public static class Serializer
	{
		public static string JsonSerializer(object? o)
		{
			if (o is null)
			{
				return "{}";
			}

			return JsonConvert.SerializeObject(o, Formatting.Indented);
		}

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
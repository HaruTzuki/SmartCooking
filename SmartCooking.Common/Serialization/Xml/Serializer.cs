using System.IO;
using System.Xml;

#nullable enable
namespace SmartCooking.Common.Serialization.Xml
{
	public static class Serializer
	{
		public static string XmlSerializer(object? o)
		{
			if (o is null)
			{
				return "";
			}

			System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(o.GetType());

			using (StringWriter? sw = new StringWriter())
			{
				using (XmlWriter writer = XmlWriter.Create(sw))
				{
					xmlSerializer.Serialize(writer, o);
					return sw.ToString();
				}
			}
		}

		public static T XmlDeserializer<T>(string? s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return default(T);
			}
			System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
			StringReader stringReader = new StringReader(s);
			return (T)xmlSerializer.Deserialize(stringReader);
		}
	}
}
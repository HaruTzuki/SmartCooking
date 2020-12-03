using System.IO;
using System.Xml;

#nullable enable

namespace SmartCooking.Common.Serialization.Xml
{
	/// <summary>
	/// Κλαση κωδικοποίησης και αποκωδικοποίησης σε και από XML κείμενο.
	/// </summary>
	public static class Serializer
	{
		/// <summary>
		/// Μετατρέπει ένα Object οποιοδήποτε τύπου σε XML κείμενο
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Μετατρέπει ένα κείμενο σε τύπο δεδομένων που του ορίζουμε.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <returns></returns>
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
namespace SIBS.ISC.Helpers
{
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using System.Xml.Serialization;

	internal static class SerializationExtension
	{
		/// <summary>
		/// The json serialize options
		/// </summary>
		internal static readonly JsonSerializerOptions JsonSerializeOptions = new()
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			PropertyNameCaseInsensitive = true,
			Converters =
			{
				new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true),
			},
			NumberHandling = JsonNumberHandling.AllowReadingFromString,
			WriteIndented = true
		};

		/// <summary>
		/// Json serialize.
		/// </summary>
		/// <typeparam name="TObjectToSerialize">The type of the object to serialize in to string.</typeparam>
		/// <param name="value">The object to serialize in to string.</param>
		/// <returns></returns>
		internal static string JsonSerialize<TObjectToSerialize>(TObjectToSerialize value) where TObjectToSerialize : class
		{
			return JsonSerializer.Serialize(value, JsonSerializeOptions);
		}

		/// <summary>
		/// Json deserialize.
		/// </summary>
		/// <typeparam name="TObjectDeserialize">The type of the object to convert from json string.</typeparam>
		/// <param name="jsonString">The json string to convert in to object.</param>
		/// <returns></returns>
		internal static TObjectDeserialize? JsonDeserialize<TObjectDeserialize>(string jsonString) where TObjectDeserialize : class
		{
			return JsonSerializer.Deserialize<TObjectDeserialize>(jsonString, JsonSerializeOptions);
		}

		/// <summary>
		/// XML serialize.
		/// </summary>
		/// <typeparam name="TObjectToSerialize">The type of the object to serialize in to string.</typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		internal static string XmlSerialize<TObjectToSerialize>(TObjectToSerialize value) where TObjectToSerialize : class
		{
			var serializer = new XmlSerializer(typeof(TObjectToSerialize));

			using (var strWriter = new StringWriter())
			{
				serializer.Serialize(strWriter, value);
				return strWriter.ToString();
			}
		}

		/// <summary>
		/// XML deserialize.
		/// </summary>
		/// <typeparam name="TObjectDeserialize">The type of the object to deserialize.</typeparam>
		/// <param name="xml">The xml string to convert in to object.</param>
		/// <returns></returns>
		/// <exception cref="System.InvalidOperationException">Can't deserialize the object '{nameof(TObjectDeserialize)}'</exception>
		internal static TObjectDeserialize XmlDeserialize<TObjectDeserialize>(this string xml) where TObjectDeserialize : class
		{
			var xmlSerializer = new XmlSerializer(typeof(TObjectDeserialize));
			var stringReader = new StringReader(xml);
			var resultingMessage = (TObjectDeserialize?)xmlSerializer.Deserialize(stringReader);

			if (resultingMessage is null)
				throw new InvalidOperationException($"Can't deserialize the object '{nameof(TObjectDeserialize)}'");

			return resultingMessage;
		}
	}
}

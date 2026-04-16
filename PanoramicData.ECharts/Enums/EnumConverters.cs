using System.Text.Json.Serialization;
using System.Text.Json;

namespace PanoramicData.ECharts;

/// <summary>JSON converter that serializes enum values as lowercase strings.</summary>
/// <typeparam name="TEnum">The enum type to convert.</typeparam>
public class LowerCaseEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct
{
	/// <inheritdoc/>
	public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException($"Deserialization is not implemented for {typeof(TEnum).Name}.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options) =>
		// Serialize the enum value as a lower-case string
		writer.WriteStringValue(value.ToString()!.ToLower());
}

/// <summary>JSON converter that serializes enum values as camelCase strings.</summary>
/// <typeparam name="TEnum">The enum type to convert.</typeparam>
public class CamelCaseEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct
{
	/// <inheritdoc/>
	public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException($"Deserialization is not implemented for {typeof(TEnum).Name}.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
	{
		var str = value.ToString()!;
		writer.WriteStringValue(char.ToLower(str[0]) + str[1..]);
	}
}

/// <summary>
/// JSON converter that serializes enum values as camelCase strings, but maps <c>True</c> and <c>False</c> enum members to JSON boolean values.
/// </summary>
/// <typeparam name="TEnum">The enum type to convert.</typeparam>
public class CamelCaseEnumConverterWithBoolean<TEnum> : JsonConverter<TEnum> where TEnum : struct
{
	/// <inheritdoc/>
	public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException($"Deserialization is not implemented for {typeof(TEnum).Name}.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
	{
		var str = value.ToString()!;
		str = char.ToLower(str[0]) + str[1..];

		switch (str)
		{
			case "true":
				writer.WriteBooleanValue(true);
				break;
			case "false":
				writer.WriteBooleanValue(false);
				break;
			default:
				writer.WriteStringValue(str);
				break;
		}
	}
}
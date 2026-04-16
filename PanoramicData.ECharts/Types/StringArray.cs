using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a single string or an array of strings.
/// Serializes as a scalar when only one value is present, otherwise as a JSON array.
/// </summary>
[JsonConverter(typeof(StringArrayConverter))]
public class StringArray
{
	/// <summary>Creates a <see cref="StringArray"/> with a single string value.</summary>
	/// <param name="value">The string value.</param>
	public StringArray(string value)
	{
		Values = [value];
	}

	/// <summary>Creates a <see cref="StringArray"/> with multiple string values.</summary>
	/// <param name="values">The array of string values.</param>
	public StringArray(string[] values)
	{
		Values = values;
	}

	/// <summary>Gets the array of string values.</summary>
	public string[]? Values { get; }

	/// <summary>Implicitly converts a string to a <see cref="StringArray"/>.</summary>
	public static implicit operator StringArray(string value)
	{
		return new StringArray(value);
	}

	/// <summary>Implicitly converts a string array to a <see cref="StringArray"/>.</summary>
	public static implicit operator StringArray(string[] values)
	{
		return new StringArray(values);
	}
}

/// <summary>JSON converter for <see cref="StringArray"/> that serializes as a single string or a JSON array.</summary>
public class StringArrayConverter : JsonConverter<StringArray>
{
	/// <inheritdoc/>
	public override StringArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for StringArray.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, StringArray value, JsonSerializerOptions options)
	{
		if (value.Values != null)
		{
			if (value.Values.Length == 1)
			{
				writer.WriteStringValue(value.Values[0]);
			}
			else
			{
				writer.WriteStartArray();
				foreach (var val in value.Values)
				{
					writer.WriteStringValue(val);
				}

				writer.WriteEndArray();
			}
		}
	}
}
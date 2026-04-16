using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a value that is a single number or string, or an array of numbers/strings.
/// Serializes as a single value when only one element is present, otherwise as an array.
/// </summary>
[JsonConverter(typeof(NumberOrStringArrayConverter))]
public class NumberOrStringArray
{
	/// <summary>Creates a <see cref="NumberOrStringArray"/> from one or more <see cref="NumberOrString"/> values.</summary>
	/// <param name="values">The values to include.</param>
	public NumberOrStringArray(params NumberOrString[] values)
	{
		Values = values;
	}

	/// <summary>Gets the array of <see cref="NumberOrString"/> values.</summary>
	public NumberOrString[] Values { get; }

	/// <summary>Implicitly converts a string to a <see cref="NumberOrStringArray"/>.</summary>
	public static implicit operator NumberOrStringArray(string value)
	{
		return new NumberOrStringArray(value);
	}

	/// <summary>Implicitly converts a double to a <see cref="NumberOrStringArray"/>.</summary>
	public static implicit operator NumberOrStringArray(double value)
	{
		return new NumberOrStringArray(value);
	}

	/// <summary>Implicitly converts a <see cref="NumberOrString"/> to a <see cref="NumberOrStringArray"/>.</summary>
	public static implicit operator NumberOrStringArray(NumberOrString value)
	{
		return new NumberOrStringArray(value);
	}

	/// <summary>Implicitly converts a <see cref="NumberOrString"/> array to a <see cref="NumberOrStringArray"/>.</summary>
	public static implicit operator NumberOrStringArray(NumberOrString[] values)
	{
		return new NumberOrStringArray(values);
	}

	/// <summary>Implicitly converts a string array to a <see cref="NumberOrStringArray"/>.</summary>
	public static implicit operator NumberOrStringArray(string[] values)
	{
		return new NumberOrStringArray(values.Select(v => new NumberOrString(v)).ToArray());
	}

	/// <summary>Implicitly converts a double array to a <see cref="NumberOrStringArray"/>.</summary>
	public static implicit operator NumberOrStringArray(double[] values)
	{
		return new NumberOrStringArray(values.Select(v => new NumberOrString(v)).ToArray());
	}
}

/// <summary>JSON converter for <see cref="NumberOrStringArray"/> that serializes as a single value or an array.</summary>
public class NumberOrStringArrayConverter : JsonConverter<NumberOrStringArray>
{
	/// <inheritdoc/>
	public override NumberOrStringArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for NumberOrStringArray.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, NumberOrStringArray value, JsonSerializerOptions options)
	{
		if (value.Values.Length == 1)
		{
			NumberOrStringConverter.Instance.Write(writer, value.Values[0], options);
		}
		else
		{
			writer.WriteStartArray();

			foreach (var val in value.Values)
				NumberOrStringConverter.Instance.Write(writer, val, options);

			writer.WriteEndArray();
		}
	}
}
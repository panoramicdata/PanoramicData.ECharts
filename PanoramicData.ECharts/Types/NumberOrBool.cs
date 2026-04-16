using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a value that can be either a number or a boolean.
/// Used for ECharts properties that accept both numeric thresholds and boolean on/off flags.
/// </summary>
[JsonConverter(typeof(NumberOrBoolConverter))]
public class NumberOrBool
{
	/// <summary>Creates a <see cref="NumberOrBool"/> from a numeric value.</summary>
	/// <param name="number">The numeric value.</param>
	public NumberOrBool(double number)
	{
		Number = number;
	}

	/// <summary>Creates a <see cref="NumberOrBool"/> from a boolean value.</summary>
	/// <param name="b">The boolean value.</param>
	public NumberOrBool(bool b)
	{
		Bool = b;
	}

	/// <summary>Gets the numeric value, or <c>null</c> if a boolean is set.</summary>
	public double? Number { get; }
	/// <summary>Gets the boolean value, or <c>null</c> if a number is set.</summary>
	public bool? Bool { get; }

	/// <summary>Implicitly converts a double to a <see cref="NumberOrBool"/>.</summary>
	public static implicit operator NumberOrBool(double number)
	{
		return new NumberOrBool(number);
	}

	/// <summary>Implicitly converts a bool to a <see cref="NumberOrBool"/>.</summary>
	public static implicit operator NumberOrBool(bool b)
	{
		return new NumberOrBool(b);
	}
}

/// <summary>JSON converter for <see cref="NumberOrBool"/> that serializes to a JSON number or boolean.</summary>
public class NumberOrBoolConverter : JsonConverter<NumberOrBool>
{
	/// <inheritdoc/>
	public override NumberOrBool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for NumberOrBool.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, NumberOrBool value, JsonSerializerOptions options)
	{
		if (value.Number != null)
		{
			writer.WriteNumberValue(value.Number.Value);
		}
		else if (value.Bool != null)
		{
			writer.WriteBooleanValue(value.Bool.Value);
		}
	}
}
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a value that can be either a number or a string.
/// Used in ECharts for properties that accept both numeric and string values (e.g., percentages like "50%" or absolute values like 100).
/// </summary>
[JsonConverter(typeof(NumberOrStringConverter))]
public class NumberOrString
{
	/// <summary>Creates a <see cref="NumberOrString"/> with a numeric value.</summary>
	/// <param name="number">The numeric value.</param>
	public NumberOrString(double number)
	{
		Number = number;
	}

	/// <summary>Creates a <see cref="NumberOrString"/> with a string value.</summary>
	/// <param name="value">The string value (e.g., a percentage like "50%").</param>
	public NumberOrString(string value)
	{
		String = value;
	}

	/// <summary>Gets the numeric value, or <c>null</c> if a string value is set.</summary>
	public double? Number { get; }
	/// <summary>Gets the string value, or <c>null</c> if a numeric value is set.</summary>
	public string? String { get; }

	/// <summary>Implicitly converts a double to a <see cref="NumberOrString"/>.</summary>
	public static implicit operator NumberOrString(double number)
	{
		return new NumberOrString(number);
	}

	/// <summary>Implicitly converts a string to a <see cref="NumberOrString"/>.</summary>
	[return: NotNullIfNotNull(nameof(str))]
	public static implicit operator NumberOrString?(string? str)
	{
		return str == null ? null : new NumberOrString(str);
	}
}

/// <summary>JSON converter for <see cref="NumberOrString"/> that serializes to a JSON number or string.</summary>
public class NumberOrStringConverter : JsonConverter<NumberOrString>
{
	private static readonly NumberOrStringConverter instance = new();

	/// <inheritdoc/>
	public override NumberOrString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for NumberOrString.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, NumberOrString value, JsonSerializerOptions options)
	{
		if (value.Number != null)
		{
			writer.WriteNumberValue(value.Number.Value);
		}
		else if (value.String != null)
		{
			writer.WriteStringValue(value.String);
		}
	}

	/// <summary>Gets the singleton instance of <see cref="NumberOrStringConverter"/>.</summary>
	public static NumberOrStringConverter Instance => instance;
}
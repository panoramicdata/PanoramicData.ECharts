using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a value that is either a single number or an array of numbers.
/// Serializes as a scalar when only one value is present, otherwise as a JSON array.
/// </summary>
[JsonConverter(typeof(NumberOrNumberArrayConverter))]
public class NumberOrNumberArray
{
	/// <summary>Creates a <see cref="NumberOrNumberArray"/> with a single number.</summary>
	/// <param name="number">The numeric value.</param>
	public NumberOrNumberArray(double number)
	{
		Numbers = [number];
	}

	/// <summary>Creates a <see cref="NumberOrNumberArray"/> with multiple numbers.</summary>
	/// <param name="numbers">The array of numeric values.</param>
	public NumberOrNumberArray(double[] numbers)
	{
		Numbers = numbers;
	}

	/// <summary>Gets the array of numeric values.</summary>
	public double[] Numbers { get; }

	/// <summary>Implicitly converts a double to a <see cref="NumberOrNumberArray"/>.</summary>
	public static implicit operator NumberOrNumberArray(double number)
	{
		return new NumberOrNumberArray(number);
	}

	/// <summary>Implicitly converts a double array to a <see cref="NumberOrNumberArray"/>.</summary>
	public static implicit operator NumberOrNumberArray(double[] numbers)
	{
		return new NumberOrNumberArray(numbers);
	}
}

/// <summary>JSON converter for <see cref="NumberOrNumberArray"/> that serializes as a scalar or JSON array.</summary>
public class NumberOrNumberArrayConverter : JsonConverter<NumberOrNumberArray>
{
	/// <inheritdoc/>
	public override NumberOrNumberArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for NumberOrNumberArray.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, NumberOrNumberArray value, JsonSerializerOptions options)
	{
		if (value.Numbers.Length == 1)
		{
			writer.WriteNumberValue(value.Numbers[0]);
		}
		else
		{
			writer.WriteStartArray();
			foreach (var val in value.Numbers)
				writer.WriteNumberValue(val);
			writer.WriteEndArray();
		}
	}
}
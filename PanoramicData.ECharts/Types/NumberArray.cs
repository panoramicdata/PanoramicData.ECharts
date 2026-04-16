using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a single number or an array of numbers.
/// Serializes as a scalar when only one value is present, otherwise as a JSON array.
/// </summary>
[JsonConverter(typeof(NumberArrayConverter))]
public class NumberArray
{
	/// <summary>Creates a <see cref="NumberArray"/> with a single number.</summary>
	/// <param name="number">The numeric value.</param>
	public NumberArray(double number)
	{
		Numbers = [number];
	}

	/// <summary>Creates a <see cref="NumberArray"/> with multiple numbers.</summary>
	/// <param name="numbers">The array of numeric values.</param>
	public NumberArray(double[] numbers)
	{
		Numbers = numbers;
	}

	/// <summary>Gets the array of numeric values.</summary>
	public double[]? Numbers { get; }

	/// <summary>Implicitly converts a double to a <see cref="NumberArray"/>.</summary>
	public static implicit operator NumberArray(double number)
	{
		return new NumberArray(number);
	}

	/// <summary>Implicitly converts a double array to a <see cref="NumberArray"/>.</summary>
	public static implicit operator NumberArray(double[] numbers)
	{
		return new NumberArray(numbers);
	}
}

/// <summary>JSON converter for <see cref="NumberArray"/> that serializes as a scalar or JSON array.</summary>
public class NumberArrayConverter : JsonConverter<NumberArray>
{
	/// <inheritdoc/>
	public override NumberArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for NumberArray.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, NumberArray value, JsonSerializerOptions options)
	{
		if (value.Numbers != null)
		{
			if (value.Numbers.Length == 1)
			{
				writer.WriteNumberValue(value.Numbers[0]);
			}
			else
			{
				writer.WriteStartArray();
				foreach (var val in value.Numbers)
				{
					writer.WriteNumberValue(val);
				}

				writer.WriteEndArray();
			}
		}
	}
}
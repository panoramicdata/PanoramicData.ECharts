using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a value that can be either a number or a JavaScript callback function.
/// Used for ECharts properties such as symbol size that can be computed dynamically.
/// </summary>
[JsonConverter(typeof(NumberOrFunctionConverter))]
public class NumberOrFunction
{
	/// <summary>Creates a <see cref="NumberOrFunction"/> from a numeric value.</summary>
	/// <param name="number">The numeric value.</param>
	public NumberOrFunction(double number)
	{
		Number = number;
	}

	/// <summary>Creates a <see cref="NumberOrFunction"/> from a JavaScript function.</summary>
	/// <param name="function">A JavaScript callback function.</param>
	public NumberOrFunction(JavascriptFunction function)
	{
		Function = function;
	}

	/// <summary>Gets the numeric value, or <c>null</c> if a function is set.</summary>
	public double? Number { get; }
	/// <summary>Gets the JavaScript function, or <c>null</c> if a number is set.</summary>
	public JavascriptFunction? Function { get; }

	/// <summary>Implicitly converts a double to a <see cref="NumberOrFunction"/>.</summary>
	public static implicit operator NumberOrFunction(double number)
	{
		return new NumberOrFunction(number);
	}

	/// <summary>Implicitly converts a <see cref="JavascriptFunction"/> to a <see cref="NumberOrFunction"/>.</summary>
	public static implicit operator NumberOrFunction(JavascriptFunction function)
	{
		return new NumberOrFunction(function);
	}
}

/// <summary>JSON converter for <see cref="NumberOrFunction"/> that serializes to a JSON number or raw JavaScript.</summary>
public class NumberOrFunctionConverter : JsonConverter<NumberOrFunction>
{
	/// <inheritdoc/>
	public override NumberOrFunction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for NumberOrFunction.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, NumberOrFunction value, JsonSerializerOptions options)
	{
		if (value.Number != null)
		{
			writer.WriteNumberValue(value.Number.Value);
		}
		else if (value.Function != null)
		{
			JavascriptFunctionConverter.Instance.Write(writer, value.Function, options);
		}
	}
}
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a value that can be a number, a string, or a JavaScript callback function.
/// Used in ECharts for highly flexible properties like label formatters and symbol sizes.
/// </summary>
[JsonConverter(typeof(NumberOrStringOrFunctionConverter))]
public class NumberOrStringOrFunction
{
	/// <summary>Creates a <see cref="NumberOrStringOrFunction"/> from a numeric value.</summary>
	/// <param name="number">The numeric value.</param>
	public NumberOrStringOrFunction(double number)
	{
		Number = number;
	}

	/// <summary>Creates a <see cref="NumberOrStringOrFunction"/> from a string value.</summary>
	/// <param name="str">The string value.</param>
	public NumberOrStringOrFunction(string str)
	{
		String = str;
	}

	/// <summary>Creates a <see cref="NumberOrStringOrFunction"/> from a JavaScript function.</summary>
	/// <param name="function">The JavaScript callback function.</param>
	public NumberOrStringOrFunction(JavascriptFunction function)
	{
		Function = function;
	}

	/// <summary>Gets the numeric value, or <c>null</c> if not set.</summary>
	public double? Number { get; }

	/// <summary>Gets the string value, or <c>null</c> if not set.</summary>
	public string? String { get; }

	/// <summary>Gets the JavaScript function, or <c>null</c> if not set.</summary>
	public JavascriptFunction? Function { get; }

	/// <summary>Implicitly converts a double to a <see cref="NumberOrStringOrFunction"/>.</summary>
	public static implicit operator NumberOrStringOrFunction(double number)
	{
		return new NumberOrStringOrFunction(number);
	}

	/// <summary>Implicitly converts a string to a <see cref="NumberOrStringOrFunction"/>.</summary>
	[return: NotNullIfNotNull(nameof(str))]
	public static implicit operator NumberOrStringOrFunction?(string? str)
	{
		return str == null ? null : new NumberOrStringOrFunction(str);
	}

	/// <summary>Implicitly converts a <see cref="JavascriptFunction"/> to a <see cref="NumberOrStringOrFunction"/>.</summary>
	public static implicit operator NumberOrStringOrFunction(JavascriptFunction function)
	{
		return new NumberOrStringOrFunction(function);
	}
}

/// <summary>JSON converter for <see cref="NumberOrStringOrFunction"/> that serializes to a number, string, or raw JavaScript.</summary>
public class NumberOrStringOrFunctionConverter : JsonConverter<NumberOrStringOrFunction>
{
	/// <inheritdoc/>
	public override NumberOrStringOrFunction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for NumberOrStringOrFunction.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, NumberOrStringOrFunction value, JsonSerializerOptions options)
	{
		if (value.Number != null)
		{
			writer.WriteNumberValue(value.Number.Value);
		}
		else if (value.String != null)
		{
			writer.WriteStringValue(value.String);
		}
		else if (value.Function != null)
		{
			JavascriptFunctionConverter.Instance.Write(writer, value.Function, options);
		}
	}
}
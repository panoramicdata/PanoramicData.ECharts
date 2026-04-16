using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a value that can be either a plain string or a JavaScript callback function.
/// Used for ECharts formatter properties that accept either a template string or a custom function.
/// </summary>
[JsonConverter(typeof(StringOrFunctionConverter))]
public class StringOrFunction
{
	/// <summary>Creates a <see cref="StringOrFunction"/> from a string value.</summary>
	/// <param name="value">The string value (e.g., a format template).</param>
	public StringOrFunction(string value)
	{
		Value = value;
	}

	/// <summary>Creates a <see cref="StringOrFunction"/> from a JavaScript function.</summary>
	/// <param name="function">A JavaScript callback function.</param>
	public StringOrFunction(JavascriptFunction function)
	{
		Function = function;
	}

	/// <summary>Gets the string value, or <c>null</c> if a function is set.</summary>
	public string? Value { get; }

	/// <summary>Gets the JavaScript function, or <c>null</c> if a string value is set.</summary>
	public JavascriptFunction? Function { get; }

	/// <summary>Implicitly converts a string to a <see cref="StringOrFunction"/>.</summary>
	public static implicit operator StringOrFunction(string value)
	{
		return new StringOrFunction(value);
	}

	/// <summary>Implicitly converts a <see cref="JavascriptFunction"/> to a <see cref="StringOrFunction"/>.</summary>
	public static implicit operator StringOrFunction(JavascriptFunction function)
	{
		return new StringOrFunction(function);
	}
}

/// <summary>JSON converter for <see cref="StringOrFunction"/> that serializes to a JSON string or raw JavaScript.</summary>
public class StringOrFunctionConverter : JsonConverter<StringOrFunction>
{
	/// <inheritdoc/>
	public override StringOrFunction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for StringOrFunction.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, StringOrFunction value, JsonSerializerOptions options)
	{
		if (value.Value != null)
		{
			writer.WriteStringValue(value.Value);
		}
		else if (value.Function != null)
		{
			JavascriptFunctionConverter.Instance.Write(writer, value.Function, options);
		}
	}
}
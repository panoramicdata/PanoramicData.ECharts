using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a value that can be either a plain object or a JavaScript callback function.
/// Used for ECharts properties that accept either a static configuration object or a dynamic function.
/// </summary>
[JsonConverter(typeof(ObjectOrFunctionConverter))]
public class ObjectOrFunction
{
	/// <summary>Creates an <see cref="ObjectOrFunction"/> from a plain object value.</summary>
	/// <param name="value">The object value.</param>
	public ObjectOrFunction(object value)
	{
		Value = value;
	}

	/// <summary>Creates an <see cref="ObjectOrFunction"/> from a JavaScript function.</summary>
	/// <param name="function">A JavaScript callback function.</param>
	public ObjectOrFunction(JavascriptFunction function)
	{
		Function = function;
	}

	/// <summary>Gets the object value, or <c>null</c> if a function is set.</summary>
	public object? Value { get; }

	/// <summary>Gets the JavaScript function, or <c>null</c> if an object value is set.</summary>
	public JavascriptFunction? Function { get; }

	/// <summary>Implicitly converts a <see cref="JavascriptFunction"/> to an <see cref="ObjectOrFunction"/>.</summary>
	public static implicit operator ObjectOrFunction(JavascriptFunction function)
	{
		return new ObjectOrFunction(function);
	}
}

/// <summary>JSON converter for <see cref="ObjectOrFunction"/> that serializes the object or raw JavaScript function.</summary>
public class ObjectOrFunctionConverter : JsonConverter<ObjectOrFunction>
{
	/// <inheritdoc/>
	public override ObjectOrFunction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for ObjectOrFunction.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, ObjectOrFunction value, JsonSerializerOptions options)
	{
		if (value.Value != null)
		{
			JsonSerializer.Serialize(writer, value.Value, value.Value.GetType(), options);
		}
		else if (value.Function != null)
		{
			JavascriptFunctionConverter.Instance.Write(writer, value.Function, options);
		}
	}
}
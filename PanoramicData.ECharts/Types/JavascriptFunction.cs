using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a raw JavaScript function to be passed as a callback to ECharts.
/// The function string is serialized as raw JavaScript, bypassing JSON string quoting.
/// </summary>
[JsonConverter(typeof(JavascriptFunctionConverter))]
public class JavascriptFunction
{
	/// <summary>Creates a <see cref="JavascriptFunction"/> from a JavaScript function string.</summary>
	/// <param name="function">The raw JavaScript function text (e.g., <c>"function(params) { return params.value; }"</c>).</param>
	public JavascriptFunction(string function)
	{
		Function = function;
	}

	/// <summary>Gets the raw JavaScript function string.</summary>
	public string Function { get; }

	/// <summary>Explicitly converts a string to a <see cref="JavascriptFunction"/>.</summary>
	public static explicit operator JavascriptFunction(string function)
		=> new JavascriptFunction(function);
}

/// <summary>JSON converter for <see cref="JavascriptFunction"/> that writes the function as raw JavaScript rather than a quoted string.</summary>
public class JavascriptFunctionConverter : JsonConverter<JavascriptFunction>
{
	private static readonly JavascriptFunctionConverter instance = new();

	/// <inheritdoc/>
	public override JavascriptFunction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for JavascriptFunction.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, JavascriptFunction value, JsonSerializerOptions options) => writer.WriteRawValue(value.Function, skipInputValidation: true);

	/// <summary>Gets the singleton instance of <see cref="JavascriptFunctionConverter"/>.</summary>
	public static JavascriptFunctionConverter Instance => instance;
}
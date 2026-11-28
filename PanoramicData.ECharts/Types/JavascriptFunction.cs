using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// A reference to a JavaScript function registered in the browser with <c>panoramicDataECharts.registerFunction(name, fn)</c>,
/// passed to ECharts wherever it accepts a callback (formatters, symbol sizes, colours and so on).
/// </summary>
/// <remarks>
/// <para>
/// Only the name crosses from .NET to the browser, so no JavaScript source is ever evaluated. Register every function
/// in a static script loaded after the PanoramicData.ECharts scripts, before the chart renders:
/// </para>
/// <code>
/// panoramicDataECharts.registerFunction("percentLabel", function (p) { return p.name + " (" + p.percent + "%)"; });
/// </code>
/// <para>Then reference it from C#: <c>Formatter = JavascriptFunction.Registered("percentLabel")</c>.</para>
/// </remarks>
[JsonConverter(typeof(JavascriptFunctionConverter))]
public class JavascriptFunction
{
	private JavascriptFunction(string name)
	{
		Name = name;
	}

	/// <summary>Gets the name the function was registered under in the browser.</summary>
	public string Name { get; }

	/// <summary>Creates a reference to a function registered in the browser under <paramref name="name"/>.</summary>
	/// <param name="name">The name passed to <c>panoramicDataECharts.registerFunction</c>.</param>
	/// <returns>A <see cref="JavascriptFunction"/> that serializes as a reference to that function.</returns>
	/// <exception cref="ArgumentException"><paramref name="name"/> is null, empty or white space.</exception>
	public static JavascriptFunction Registered(string name)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(name);
		return new JavascriptFunction(name);
	}
}

/// <summary>JSON converter for <see cref="JavascriptFunction"/>: writes the marker object <c>{"$pdFn":"name"}</c>, which the browser resolves to the registered function.</summary>
public class JavascriptFunctionConverter : JsonConverter<JavascriptFunction>
{
	internal const string MarkerProperty = "$pdFn";

	private static readonly JavascriptFunctionConverter instance = new();

	/// <inheritdoc/>
	public override JavascriptFunction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for JavascriptFunction.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, JavascriptFunction value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteString(MarkerProperty, value.Name);
		writer.WriteEndObject();
	}

	/// <summary>Gets the singleton instance of <see cref="JavascriptFunctionConverter"/>.</summary>
	public static JavascriptFunctionConverter Instance => instance;
}

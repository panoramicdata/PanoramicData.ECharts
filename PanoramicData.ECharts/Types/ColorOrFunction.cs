using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a value that can be either a color or a JavaScript callback function that returns a color.
/// Used for dynamic color assignment in ECharts series.
/// </summary>
[JsonConverter(typeof(ColorOrFunctionConverter))]
public class ColorOrFunction
{
	/// <summary>Creates a <see cref="ColorOrFunction"/> from a color value.</summary>
	/// <param name="color">The color value.</param>
	public ColorOrFunction(Color color)
	{
		Color = color;
	}

	/// <summary>Creates a <see cref="ColorOrFunction"/> from a JavaScript function.</summary>
	/// <param name="function">A JavaScript function that returns a color.</param>
	public ColorOrFunction(JavascriptFunction function)
	{
		Function = function;
	}

	/// <summary>Gets the color value, or <c>null</c> if a function is set.</summary>
	public Color? Color { get; }
	/// <summary>Gets the JavaScript function, or <c>null</c> if a color is set.</summary>
	public JavascriptFunction? Function { get; }

	/// <summary>Implicitly converts a string to a <see cref="ColorOrFunction"/>.</summary>
	public static implicit operator ColorOrFunction(string color)
	{
		return new ColorOrFunction(new Color(color));
	}

	/// <summary>Implicitly converts a <see cref="Color"/> to a <see cref="ColorOrFunction"/>.</summary>
	public static implicit operator ColorOrFunction(Color color)
	{
		return new ColorOrFunction(color);
	}

	/// <summary>Implicitly converts a <see cref="JavascriptFunction"/> to a <see cref="ColorOrFunction"/>.</summary>
	public static implicit operator ColorOrFunction(JavascriptFunction function)
	{
		return new ColorOrFunction(function);
	}
}

/// <summary>JSON converter for <see cref="ColorOrFunction"/> that serializes to a color value or raw JavaScript function.</summary>
public class ColorOrFunctionConverter : JsonConverter<ColorOrFunction>
{
	/// <inheritdoc/>
	public override ColorOrFunction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for ColorOrFunction.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, ColorOrFunction value, JsonSerializerOptions options)
	{
		if (value.Color != null)
		{
			ColorConverter.Instance.Write(writer, value.Color, options);
		}
		else if (value.Function != null)
		{
			JavascriptFunctionConverter.Instance.Write(writer, value.Function, options);
		}
	}
}
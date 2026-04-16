using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a single color or an array of colors.
/// Serializes as a single color value when only one color is present, otherwise as an array.
/// Used for palette-style color assignments in ECharts.
/// </summary>
[JsonConverter(typeof(ColorArrayConverter))]
public class ColorArray
{
	/// <summary>Creates a <see cref="ColorArray"/> with a single color.</summary>
	/// <param name="color">The color value.</param>
	public ColorArray(Color color)
	{
		Colors = [color];
	}

	/// <summary>Creates a <see cref="ColorArray"/> with multiple colors.</summary>
	/// <param name="colors">The array of color values.</param>
	public ColorArray(Color[] colors)
	{
		Colors = colors;
	}

	/// <summary>Gets the array of colors.</summary>
	public Color[]? Colors { get; }

	/// <summary>Implicitly converts a string to a <see cref="ColorArray"/>.</summary>
	public static implicit operator ColorArray(string color)
	{
		return new ColorArray(color);
	}

	/// <summary>Implicitly converts a <see cref="Color"/> to a <see cref="ColorArray"/>.</summary>
	public static implicit operator ColorArray(Color color)
	{
		return new ColorArray(color);
	}

	/// <summary>Implicitly converts a <see cref="Color"/> array to a <see cref="ColorArray"/>.</summary>
	public static implicit operator ColorArray(Color[] colors)
	{
		return new ColorArray(colors);
	}
}

/// <summary>JSON converter for <see cref="ColorArray"/> that serializes as a single color or a JSON array.</summary>
public class ColorArrayConverter : JsonConverter<ColorArray>
{
	/// <inheritdoc/>
	public override ColorArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for ColorArray.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, ColorArray value, JsonSerializerOptions options)
	{
		if (value.Colors != null)
		{
			if (value.Colors.Length == 1)
			{
				ColorConverter.Instance.Write(writer, value.Colors[0], options);
			}
			else
			{
				writer.WriteStartArray();
				foreach (var val in value.Colors)
				{
					ColorConverter.Instance.Write(writer, val, options);
				}

				writer.WriteEndArray();
			}
		}
	}
}
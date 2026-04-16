using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a legend or toolbox icon, which can be either a built-in type or a custom URL/data-URI.
/// See https://echarts.apache.org/en/option.html#legend.icon
/// </summary>
[JsonConverter(typeof(IconConverter))]
public class Icon
{
	/// <summary>Creates an <see cref="Icon"/> from a custom image URL or SVG path.</summary>
	/// <param name="url">A URL, data URI, or SVG path string.</param>
	public Icon(string url)
	{
		Url = url;
	}

	/// <summary>Creates an <see cref="Icon"/> from a built-in icon type.</summary>
	/// <param name="type">The built-in icon shape.</param>
	public Icon(IconType type)
	{
		Type = type;
	}

	/// <summary>Gets the custom URL or SVG path for the icon, if set.</summary>
	public string? Url { get; }
	/// <summary>Gets the built-in icon type, if set.</summary>
	public IconType? Type { get; }

	/// <summary>Implicitly converts a string URL to an <see cref="Icon"/>.</summary>
	public static implicit operator Icon(string url)
	{
		return new Icon(url);
	}

	/// <summary>Implicitly converts an <see cref="IconType"/> to an <see cref="Icon"/>.</summary>
	public static implicit operator Icon(IconType type)
	{
		return new Icon(type);
	}
}

/// <summary>Built-in icon shapes available for legend and toolbox icons.</summary>
public enum IconType
{
	/// <summary>A circular icon shape.</summary>
	Circle,
	/// <summary>A rectangular icon shape.</summary>
	Rect,
	/// <summary>A rounded rectangle icon shape.</summary>
	RoundRect,
	/// <summary>A triangular icon shape.</summary>
	Triangle,
	/// <summary>A diamond icon shape.</summary>
	Diamond,
	/// <summary>A pin/marker icon shape.</summary>
	Pin,
	/// <summary>An arrow icon shape.</summary>
	Arrow,
	/// <summary>No icon is displayed.</summary>
	None
}

/// <summary>JSON converter for <see cref="Icon"/> that serializes to a lowercase string or URL.</summary>
public class IconConverter : JsonConverter<Icon>
{
	private static readonly IconConverter instance = new();

	/// <inheritdoc/>
	public override Icon Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for Icon.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, Icon value, JsonSerializerOptions options)
	{
		if (value.Type != null)
		{
			writer.WriteStringValue(value.Type!.ToString()!.ToLower());
		}
		else if (value.Url != null)
		{
			writer.WriteStringValue(value.Url);
		}
	}

	/// <summary>Gets the singleton instance of <see cref="IconConverter"/>.</summary>
	public static IconConverter Instance => instance;
}
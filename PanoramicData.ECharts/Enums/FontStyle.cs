using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the style of the font for chart text elements.
/// See https://developer.mozilla.org/en-US/docs/Web/CSS/font-style
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<FontStyle>))]
public enum FontStyle
{
	/// <summary>Normal upright font style.</summary>
	Normal,
	/// <summary>Italic font style, using a specially designed italic variant if available.</summary>
	Italic,
	/// <summary>Oblique font style, using a slanted version of the normal font.</summary>
	Oblique
}

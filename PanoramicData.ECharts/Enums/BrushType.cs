using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies whether drawing uses stroke (outline) or fill rendering for graphic elements.
/// See https://echarts.apache.org/en/option.html#brush
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<BrushType>))]
public enum BrushType
{
	/// <summary>Renders only the outline (stroke) of the shape.</summary>
	Stroke,
	/// <summary>Renders the filled area of the shape.</summary>
	Fill
}

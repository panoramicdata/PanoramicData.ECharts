using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the shape of the radar chart background.
/// See https://echarts.apache.org/en/option.html#radar.shape
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<RadarShape>))]
public enum RadarShape
{
	/// <summary>The radar background is drawn as a polygon (default).</summary>
	Polygon,
	/// <summary>The radar background is drawn as a circle.</summary>
	Circle
}
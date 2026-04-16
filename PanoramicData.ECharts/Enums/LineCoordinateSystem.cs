using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// see https://echarts.apache.org/en/option.html#series-line.coordinateSystem
/// </summary>
[JsonConverter(typeof(LowerCaseEnumConverter<LineCoordinateSystem>))]
public enum LineCoordinateSystem
{
	/// <summary>Uses a 2D Cartesian coordinate system (x/y axes). Default for most chart types.</summary>
	Cartesian2D,
	/// <summary>Uses a polar coordinate system (angle and radius axes).</summary>
	Polar
}

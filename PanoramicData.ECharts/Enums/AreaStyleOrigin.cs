using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the origin of the area style fill in a line or bar chart.
/// See https://echarts.apache.org/en/option.html#series-line.areaStyle.origin
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverterWithBoolean<AreaStyleOrigin>))]
public enum AreaStyleOrigin
{
	/// <summary>ECharts automatically determines the fill origin based on the data range.</summary>
	Auto,
	/// <summary>The area fill originates from the start (minimum value) of the axis.</summary>
	Start,
	/// <summary>The area fill originates from the end (maximum value) of the axis.</summary>
	End
}

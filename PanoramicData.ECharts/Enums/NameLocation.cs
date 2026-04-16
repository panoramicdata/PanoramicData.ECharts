using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the location of the axis name label along the axis.
/// See https://echarts.apache.org/en/option.html#xAxis.nameLocation
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<NameLocation>))]
public enum NameLocation
{
	/// <summary>The axis name is displayed at the start of the axis.</summary>
	Start,
	/// <summary>The axis name is displayed at the middle of the axis.</summary>
	Middle,
	/// <summary>The axis name is displayed at the end of the axis.</summary>
	End
}
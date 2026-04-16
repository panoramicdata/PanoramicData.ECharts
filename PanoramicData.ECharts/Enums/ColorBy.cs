using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies how colors are assigned to chart elements — by series or by individual data point.
/// See https://echarts.apache.org/en/option.html#series-bar.colorBy
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<ColorBy>))]
public enum ColorBy
{
	/// <summary>All data in the same series share the same color from the color palette.</summary>
	Series,
	/// <summary>Each data item in a series gets a different color from the color palette.</summary>
	Data
}

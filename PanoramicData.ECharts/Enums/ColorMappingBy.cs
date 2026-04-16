using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies how colors are mapped in treemap and sunburst charts.
/// See https://echarts.apache.org/en/option.html#series-treemap.colorMappingBy
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<ColorMappingBy>))]
public enum ColorMappingBy
{
	/// <summary>Color is assigned based on the node's index in the data array.</summary>
	Index,
	/// <summary>Color is assigned based on the node's numeric value.</summary>
	Value,
	/// <summary>Color is assigned based on the node's unique identifier.</summary>
	Id
}

using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the type of legend component.
/// See https://echarts.apache.org/en/option.html#legend.type
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<LegendType>))]
public enum LegendType
{
	/// <summary>A plain legend that shows all items at once. Use for a small number of series.</summary>
	Plain,
	/// <summary>A scrollable legend with pagination for a large number of series.</summary>
	Scroll
}
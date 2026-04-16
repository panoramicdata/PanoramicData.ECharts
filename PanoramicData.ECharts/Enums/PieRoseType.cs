using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the rose diagram (Nightingale chart) type for pie series.
/// See https://echarts.apache.org/en/option.html#series-pie.roseType
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<PieRoseType>))]
public enum PieRoseType
{
	/// <summary>Each sector has different outer radius. The area of a sector is proportional to the data value.</summary>
	Radius,
	/// <summary>Each sector has the same outer radius, but the sectors sweep different angles proportional to data.</summary>
	Area,
	/// <summary>Rose diagram is enabled. Equivalent to <c>true</c>.</summary>
	True,
	/// <summary>Rose diagram is disabled; uses a standard pie chart. Equivalent to <c>false</c>.</summary>
	False
}
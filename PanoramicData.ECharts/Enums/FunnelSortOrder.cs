using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the sort order for funnel chart data.
/// See https://echarts.apache.org/en/option.html#series-funnel.sort
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<FunnelSortOrder>))]
public enum FunnelSortOrder
{
	/// <summary>Data is sorted in ascending order (smallest value at the top).</summary>
	Ascending,
	/// <summary>Data is sorted in descending order (largest value at the top). This is the default.</summary>
	Descending,
	/// <summary>Data is not sorted; the original order is preserved.</summary>
	None
}

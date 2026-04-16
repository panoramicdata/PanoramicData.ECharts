using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the sort order for data in chart series such as treemap.
/// See https://echarts.apache.org/en/option.html#series-treemap.sort
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<SortOrder>))]
public enum SortOrder
{
	/// <summary>Data is sorted in ascending order.</summary>
	Asc,
	/// <summary>Data is sorted in descending order.</summary>
	Desc,
	/// <summary>Data is not sorted; the original order is used.</summary>
	None
}

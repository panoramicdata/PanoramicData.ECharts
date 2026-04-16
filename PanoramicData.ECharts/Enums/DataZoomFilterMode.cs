using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies how data outside the DataZoom range is handled.
/// See https://echarts.apache.org/en/option.html#dataZoom-inside.filterMode
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<DataZoomFilterMode>))]
public enum DataZoomFilterMode
{
	/// <summary>Data items outside the zoom window are filtered out and the axis range is set to the zoom window.</summary>
	Filter,
	/// <summary>Similar to Filter, but the axis range is not changed when data in other series is still visible.</summary>
	WeakFilter,
	/// <summary>Data items outside the window are set to empty (null) rather than removed.</summary>
	Empty,
	/// <summary>No filtering is applied; data items outside the zoom window are still rendered.</summary>
	None
}

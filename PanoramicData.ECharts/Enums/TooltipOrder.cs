using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// 'seriesAsc': Base on series declaration, ascending order tooltip.
/// 'seriesDesc': Base on series declaration, descending order tooltip.
/// 'valueAsc': Base on value, ascending order tooltip, only for numberic value.
/// 'valueDesc': Base on value, descending order tooltip, only for numberic value.
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<TooltipOrder>))]
public enum TooltipOrder
{
	/// <summary>Orders tooltip entries by series declaration in ascending order.</summary>
	SeriesAsc,
	/// <summary>Orders tooltip entries by series declaration in descending order.</summary>
	SeriesDesc,
	/// <summary>Orders tooltip entries by value in ascending order.</summary>
	ValueAsc,
	/// <summary>Orders tooltip entries by value in descending order.</summary>
	ValueDesc,
}

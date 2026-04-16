using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the behavior when a node in a sunburst chart is clicked.
/// See https://echarts.apache.org/en/option.html#series-sunburst.nodeClick
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverterWithBoolean<SunburstNodeClick>))]
public enum SunburstNodeClick
{
	/// <summary>Click interaction is enabled (zooms to root). Equivalent to <c>true</c>.</summary>
	True,
	/// <summary>Click interaction is disabled. Equivalent to <c>false</c>.</summary>
	False,
	/// <summary>Clicking a node zooms to show the path from the root to that node.</summary>
	RootToNode,
	/// <summary>Clicking a node opens the URL defined in the node's link property.</summary>
	Link
}
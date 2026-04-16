using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the behavior when a node in a treemap chart is clicked.
/// See https://echarts.apache.org/en/option.html#series-treemap.nodeClick
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverterWithBoolean<TreemapNodeClick>))]
public enum TreemapNodeClick
{
	/// <summary>Click interaction is enabled. Equivalent to <c>true</c>.</summary>
	True,
	/// <summary>Click interaction is disabled. Equivalent to <c>false</c>.</summary>
	False,
	/// <summary>Clicking a node zooms into that node to show its children.</summary>
	ZoomToNode,
	/// <summary>Clicking a node opens the URL defined in the node's link property.</summary>
	Link
}
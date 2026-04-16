using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the layout type used to draw edges between nodes in a tree chart.
/// See https://echarts.apache.org/en/option.html#series-tree.layout
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<TreeLayout>))]
public enum TreeLayout
{
	/// <summary>Edges are drawn as curved bezier lines.</summary>
	Curve,
	/// <summary>Edges are drawn as polylines (straight segments with bends).</summary>
	Polyline
}

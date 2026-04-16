using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the shape of edges connecting nodes in a tree chart.
/// See https://echarts.apache.org/en/option.html#series-tree.edgeShape
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<TreeEdgeShape>))]
public enum TreeEdgeShape
{
	/// <summary>Edges are drawn as right-angle (orthogonal/elbow) connectors.</summary>
	Orthogonal,
	/// <summary>Edges are drawn as curved arcs radiating from the root.</summary>
	Radial
}
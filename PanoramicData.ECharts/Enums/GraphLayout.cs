using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the layout algorithm for graph (network) series.
/// See https://echarts.apache.org/en/option.html#series-graph.layout
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<GraphLayout>))]
public enum GraphLayout
{
	/// <summary>No automatic layout; node positions must be specified manually via x and y coordinates.</summary>
	None,
	/// <summary>Uses a force-directed layout algorithm to automatically position nodes.</summary>
	Force,
	/// <summary>Arranges nodes in a circular layout.</summary>
	Circular
}

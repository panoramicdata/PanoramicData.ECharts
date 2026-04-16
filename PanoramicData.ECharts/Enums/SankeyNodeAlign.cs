using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the alignment of Sankey chart nodes within their layer.
/// See https://echarts.apache.org/en/option.html#series-sankey.nodeAlign
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<SankeyNodeAlign>))]
public enum SankeyNodeAlign
{
	/// <summary>Nodes are aligned to fill the full extent of each layer (justified).</summary>
	Justify,
	/// <summary>Nodes are aligned to the left (or top) of each layer.</summary>
	Left,
	/// <summary>Nodes are aligned to the right (or bottom) of each layer.</summary>
	Right
}

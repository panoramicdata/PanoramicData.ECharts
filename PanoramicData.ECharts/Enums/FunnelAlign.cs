using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the horizontal alignment of funnel chart items.
/// See https://echarts.apache.org/en/option.html#series-funnel.funnelAlign
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<FunnelAlign>))]
public enum FunnelAlign
{
	/// <summary>Funnel items are aligned to the left edge.</summary>
	Left,
	/// <summary>Funnel items are centered horizontally.</summary>
	Center,
	/// <summary>Funnel items are aligned to the right edge.</summary>
	Right
}

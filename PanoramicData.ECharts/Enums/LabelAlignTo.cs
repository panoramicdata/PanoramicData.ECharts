using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies how labels in a pie chart align relative to their leader lines.
/// See https://echarts.apache.org/en/option.html#series-pie.label.alignTo
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<LabelAlignTo>))]
public enum LabelAlignTo
{
	/// <summary>Labels are aligned to the label line (guide line connecting element to label).</summary>
	LabelLine,
	/// <summary>Labels are aligned to the chart container edge.</summary>
	Edge
}

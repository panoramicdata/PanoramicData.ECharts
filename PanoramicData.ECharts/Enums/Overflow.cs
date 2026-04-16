using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// See https://echarts.apache.org/en/option.html#grid.tooltip.axisPointer.label.overflow
/// Determine how to display the text when it's overflow. Available when width is set.
/// 'truncate' Truncate the text and trailing with ellipsis.
/// 'break' Break by word
/// 'breakAll' Break by character.
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<Overflow>))]
public enum Overflow
{
	/// <summary>Truncates overflowing text and appends an ellipsis.</summary>
	Truncate,
	/// <summary>Breaks overflowing text at word boundaries.</summary>
	Break,
	/// <summary>Breaks overflowing text at any character.</summary>
	BreakAll
}
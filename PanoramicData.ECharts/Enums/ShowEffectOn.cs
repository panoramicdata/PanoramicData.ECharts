using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies when the ripple/wave effect is shown for effect scatter series.
/// See https://echarts.apache.org/en/option.html#series-effectScatter.showEffectOn
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<ShowEffectOn>))]
public enum ShowEffectOn
{
	/// <summary>The effect is shown continuously while rendering.</summary>
	Render,
	/// <summary>The effect is shown only when the element is hovered (emphasized).</summary>
	Emphasis
}

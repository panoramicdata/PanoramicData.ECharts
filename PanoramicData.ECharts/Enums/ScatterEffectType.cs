using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the type of animated effect shown for effect scatter series.
/// See https://echarts.apache.org/en/option.html#series-effectScatter.effectType
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<ScatterEffectType>))]
public enum ScatterEffectType
{
	/// <summary>A ripple/wave animation emanates from the scatter point.</summary>
	Ripple
}

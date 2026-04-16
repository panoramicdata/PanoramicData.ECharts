using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the animation type used when chart data is updated.
/// See https://echarts.apache.org/en/option.html#series-bar.animationTypeUpdate
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<AnimationTypeUpdate>))]
public enum AnimationTypeUpdate
{
	/// <summary>Elements transition smoothly from their previous positions to their new positions.</summary>
	Transition,
	/// <summary>Elements expand outward during the update animation.</summary>
	Expansion
}
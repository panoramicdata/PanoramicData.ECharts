using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the initial animation type for a series.
/// See https://echarts.apache.org/en/option.html#series-bar.animationType
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<AnimationType>))]
public enum AnimationType
{
	/// <summary>The data expands outward from the origin point during the initial animation.</summary>
	Expansion,
	/// <summary>The data scales from zero to full size during the initial animation.</summary>
	Scale
}

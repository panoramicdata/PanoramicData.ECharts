using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the scope of the blur effect when emphasis (hover) is activated.
/// See https://echarts.apache.org/en/option.html#series-bar.emphasis.blurScope
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<EmphasisBlurScope>))]
public enum EmphasisBlurScope
{
	/// <summary>Only elements within the same coordinate system are blurred when hovering.</summary>
	CoordinateSystem,
	/// <summary>Only elements within the same series are blurred when hovering.</summary>
	Series,
	/// <summary>All other elements in the entire chart are blurred when hovering.</summary>
	Global
}

using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies which elements are highlighted and which are blurred on hover.
/// See https://echarts.apache.org/en/option.html#series-bar.emphasis.focus
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<EmphasisFocus>))]
public enum EmphasisFocus
{
	/// <summary>No element is blurred when hovering; all elements remain fully visible.</summary>
	None,
	/// <summary>Only the hovered element is highlighted; other elements in the same series are blurred.</summary>
	Self,
	/// <summary>All elements in the same series are highlighted; elements in other series are blurred.</summary>
	Series
}
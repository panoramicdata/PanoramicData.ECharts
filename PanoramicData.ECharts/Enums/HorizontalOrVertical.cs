using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies a simple horizontal or vertical orientation for chart elements such as sliders and scrollbars.
/// See https://echarts.apache.org/en/option.html#dataZoom-slider.orient
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<HorizontalOrVertical>))]
public enum HorizontalOrVertical
{
	/// <summary>The element is laid out horizontally.</summary>
	Horizontal,
	/// <summary>The element is laid out vertically.</summary>
	Vertical
}

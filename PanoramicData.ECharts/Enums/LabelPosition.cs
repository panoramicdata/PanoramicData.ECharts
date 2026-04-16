using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the position of a label relative to its associated chart element.
/// See https://echarts.apache.org/en/option.html#series-bar.label.position
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<LabelPosition>))]
public enum LabelPosition
{
	/// <summary>Label is positioned above the element.</summary>
	Top,
	/// <summary>Label is positioned to the left of the element.</summary>
	Left,
	/// <summary>Label is positioned to the right of the element.</summary>
	Right,
	/// <summary>Label is positioned below the element.</summary>
	Bottom,
	/// <summary>Label is positioned inside the element, centered.</summary>
	Inside,
	/// <summary>Label is positioned inside the element, aligned left.</summary>
	InsideLeft,
	/// <summary>Label is positioned inside the element, aligned right.</summary>
	InsideRight,
	/// <summary>Label is positioned inside the element, aligned top.</summary>
	InsideTop,
	/// <summary>Label is positioned inside the element, aligned bottom.</summary>
	InsideBottom,
	/// <summary>Label is positioned inside the element, at the top-left corner.</summary>
	InsideTopLeft,
	/// <summary>Label is positioned inside the element, at the bottom-left corner.</summary>
	InsideBottomLeft,
	/// <summary>Label is positioned inside the element, at the top-right corner.</summary>
	InsideTopRight,
	/// <summary>Label is positioned inside the element, at the bottom-right corner.</summary>
	InsideBottomRight,
	/// <summary>Label is positioned outside the element (typically used for pie/funnel charts).</summary>
	Outside,
	/// <summary>Label is positioned at the center of the element.</summary>
	Center
}

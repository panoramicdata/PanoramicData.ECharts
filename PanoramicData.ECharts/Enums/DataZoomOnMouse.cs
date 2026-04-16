using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies which mouse action activates the inside DataZoom component.
/// See https://echarts.apache.org/en/option.html#dataZoom-inside.zoomOnMouseWheel
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverterWithBoolean<DataZoomOnMouse>))]
public enum DataZoomOnMouse
{
	/// <summary>The zoom/move action is activated without any modifier key. Equivalent to <c>true</c>.</summary>
	True,
	/// <summary>The zoom/move action is not activated. Equivalent to <c>false</c>.</summary>
	False,
	/// <summary>The zoom/move action is activated when the Shift key is held.</summary>
	Shift,
	/// <summary>The zoom/move action is activated when the Ctrl key is held.</summary>
	Ctrl,
	/// <summary>The zoom/move action is activated when the Alt key is held.</summary>
	Alt
}

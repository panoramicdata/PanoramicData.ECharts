using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the type of the axis pointer indicator shown on hover.
/// See https://echarts.apache.org/en/option.html#tooltip.axisPointer.type
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<AxisPointerType>))]
public enum AxisPointerType
{
	/// <summary>Displays a straight line as the axis pointer.</summary>
	Line,
	/// <summary>Displays a shaded region as the axis pointer.</summary>
	Shadow,
	/// <summary>No axis pointer is displayed.</summary>
	None,
	/// <summary>Displays crosshair lines (both horizontal and vertical) as the axis pointer.</summary>
	Cross
}

using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the axis pointer display status for tooltips.
/// See https://echarts.apache.org/en/option.html#tooltip.axisPointer.status
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverterWithBoolean<AxisPointerStatus>))]
public enum AxisPointerStatus
{
	/// <summary>The axis pointer is shown. Equivalent to <c>true</c>.</summary>
	True,
	/// <summary>The axis pointer is hidden. Equivalent to <c>false</c>.</summary>
	False,
	/// <summary>The axis pointer is shown.</summary>
	Show,
	/// <summary>The axis pointer is hidden.</summary>
	Hide
}

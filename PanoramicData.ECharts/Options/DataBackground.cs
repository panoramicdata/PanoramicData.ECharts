using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Configures the DataBackground option for ECharts.
/// See https://echarts.apache.org/en/option.html for full documentation.
/// </summary>
public partial class DataBackground
{
	/// <summary>
	/// Line style of data shadow
	/// </summary>
	[JsonPropertyName("lineStyle")]
	public LineStyle? LineStyle { get; set; }

	/// <summary>
	/// Area style of data shadow
	/// </summary>
	[JsonPropertyName("areaStyle")]
	public AreaStyle? AreaStyle { get; set; }

}

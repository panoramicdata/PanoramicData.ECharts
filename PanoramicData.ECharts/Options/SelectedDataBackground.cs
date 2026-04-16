using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Configures the SelectedDataBackground option for ECharts.
/// See https://echarts.apache.org/en/option.html for full documentation.
/// </summary>
public partial class SelectedDataBackground
{
	/// <summary>
	/// Line style of selected data shadow.
	/// </summary>
	[JsonPropertyName("lineStyle")]
	public LineStyle? LineStyle { get; set; }

	/// <summary>
	/// Area style of selected data shadow.
	/// </summary>
	[JsonPropertyName("areaStyle")]
	public AreaStyle? AreaStyle { get; set; }

}

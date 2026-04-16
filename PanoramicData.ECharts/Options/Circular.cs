
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Configures the Circular option for ECharts.
/// See https://echarts.apache.org/en/option.html for full documentation.
/// </summary>
public partial class Circular
{
	/// <summary>
	/// Whether to rotate the label automatically.
	/// </summary>
	[JsonPropertyName("rotateLabel")]
	[DefaultValue(false)]
	public bool? RotateLabel { get; set; }

}

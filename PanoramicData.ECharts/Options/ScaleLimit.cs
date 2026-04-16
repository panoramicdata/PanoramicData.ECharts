using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Configures the ScaleLimit option for ECharts.
/// See https://echarts.apache.org/en/option.html for full documentation.
/// </summary>
public partial class ScaleLimit
{
	/// <summary>
	/// Minimum scaling
	/// </summary>
	[JsonPropertyName("min")]
	public double? Min { get; set; }

	/// <summary>
	/// Maximum scaling
	/// </summary>
	[JsonPropertyName("max")]
	public double? Max { get; set; }

}

using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Configures the Rich option for ECharts.
/// See https://echarts.apache.org/en/option.html for full documentation.
/// </summary>
public partial class Rich
{
	/// <summary>
	/// 
	/// </summary>
	[JsonPropertyName("RichStyleName")]
	public RichStyleName? RichStyleName { get; set; }

}

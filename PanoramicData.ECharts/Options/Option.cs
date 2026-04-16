using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Configures the Option for ECharts.
/// See https://echarts.apache.org/en/option.html for full documentation.
/// </summary>
public partial class Option
{
	/// <summary>
	/// 
	/// </summary>
	[JsonPropertyName("line")]
	public Line? Line { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[JsonPropertyName("bar")]
	public Bar? Bar { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[JsonPropertyName("stack")]
	public Stack? Stack { get; set; }

}

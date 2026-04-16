using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Configures the YAxisData option for ECharts.
/// See https://echarts.apache.org/en/option.html for full documentation.
/// </summary>
public partial class YAxisData
{
	/// <summary>
	/// Name of a category.
	/// </summary>
	[JsonPropertyName("value")]
	public string? Value { get; set; }

	/// <summary>
	/// Text style of the category.
	/// </summary>
	[JsonPropertyName("textStyle")]
	public TextStyle? TextStyle { get; set; }

}

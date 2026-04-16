using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// axisPointers can be linked to each other. The term "link" represents that axes are synchronized and move together. Axes are linked according to the value of axisPointer.
/// See https://echarts.apache.org/en/option.html#axisPointer.link
/// </summary>
public class AxisPointerLink
{

	/// <summary>Gets or sets the linked x-axis indexes.</summary>
	[JsonPropertyName("xAxisIndex")]
	public MultiIndex? XAxisIndex { get; set; }

	/// <summary>Gets or sets the linked x-axis names.</summary>
	[JsonPropertyName("xAxisName")]
	public string[]? XAxisName { get; set; }

	/// <summary>Gets or sets the linked x-axis identifiers.</summary>
	[JsonPropertyName("xAxisId")]
	public string[]? XAxisId { get; set; }

	/// <summary>Gets or sets the linked y-axis indexes.</summary>
	[JsonPropertyName("yAxisIndex")]
	public MultiIndex? YAxisIndex { get; set; }

	/// <summary>Gets or sets the linked y-axis names.</summary>
	[JsonPropertyName("yAxisName")]
	public string[]? YAxisName { get; set; }

	/// <summary>Gets or sets the linked y-axis identifiers.</summary>
	[JsonPropertyName("yAxisId")]
	public string[]? YAxisId { get; set; }

	/// <summary>Gets or sets the linked angle axes.</summary>
	[JsonPropertyName("angleAxis")]
	public string[]? AngleAxis { get; set; }

	/// <summary>Gets or sets the mapper function used to translate linked values.</summary>
	[JsonPropertyName("mapper")]
	public JavascriptFunction? Mapper { get; set; }
}

using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// See https://echarts.apache.org/en/option.html#toolbox.feature.magicType.seriesIndex
/// </summary>
public partial class SeriesIndex
{
	/// <summary>Gets or sets the series indexes used when switching to line mode.</summary>
	public object? Line { get; set; }

	/// <summary>Gets or sets the series indexes used when switching to bar mode.</summary>
	[JsonPropertyName("bar")]
	public object? Bar { get; set; }

}

using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Configures the Media option for ECharts.
/// See https://echarts.apache.org/en/option.html for full documentation.
/// </summary>
public partial class Media
{
	/// <summary>
	/// If more than one properties used, it means "and".
	/// </summary>
	[JsonPropertyName("query")]
	public Query? Query { get; set; }

	/// <summary>
	/// Each item of this array is an echarts option ( ECUnitOption ).
	/// It will be applied when this query is matched.
	/// </summary>
	[JsonPropertyName("option")]
	public Option? Option { get; set; }

}

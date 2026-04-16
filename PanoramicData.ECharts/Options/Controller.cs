using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Configures the Controller option for ECharts.
/// See https://echarts.apache.org/en/option.html for full documentation.
/// </summary>
public partial class Controller
{
	/// <summary>
	/// Define visual channels that will mapped from dataValues that are in selected range .
	/// User can interact with visualMap component and make a seleced range by mouse or touch.
	///  
	/// See available configurations in visualMap-continuous.inRange
	/// </summary>
	[JsonPropertyName("inRange")]
	public VisualMapRange? InRange { get; set; }

	/// <summary>
	/// Define visual channels that will mapped from dataValues that are out of selected range .
	/// User can interact with visualMap component and make a seleced range by mouse or touch.
	///  
	/// See available configurations in visualMap-continuous.inRange
	/// </summary>
	[JsonPropertyName("outOfRange")]
	public VisualMapRange? OutOfRange { get; set; }

}

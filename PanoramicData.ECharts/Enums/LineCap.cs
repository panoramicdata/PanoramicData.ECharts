using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the shape of the end caps of lines and open paths.
/// See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineCap
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<LineCap>))]
public enum LineCap
{
	/// <summary>The ends of lines are flat, ending exactly at the endpoint.</summary>
	Butt,
	/// <summary>The ends of lines are rounded, adding a semicircle beyond the endpoint.</summary>
	Round,
	/// <summary>The ends of lines are squared, adding a rectangle beyond the endpoint.</summary>
	Square
}

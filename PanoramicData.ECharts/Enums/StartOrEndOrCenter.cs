using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies a position at the start, end, or center of a range or axis.
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<StartOrEndOrCenter>))]
public enum StartOrEndOrCenter
{
	/// <summary>The start of the range or axis.</summary>
	Start,
	/// <summary>The end of the range or axis.</summary>
	End,
	/// <summary>The center of the range or axis.</summary>
	Center
}

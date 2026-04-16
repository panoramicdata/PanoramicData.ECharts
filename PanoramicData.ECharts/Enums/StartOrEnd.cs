using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies a position at either the start or end of a range or axis.
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<StartOrEnd>))]
public enum StartOrEnd
{
	/// <summary>The start of the range or axis.</summary>
	Start,
	/// <summary>The end of the range or axis.</summary>
	End
}

using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies a simple left or right position.
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<LeftOrRight>))]
public enum LeftOrRight
{
	/// <summary>Positioned on the left side.</summary>
	Left,
	/// <summary>Positioned on the right side.</summary>
	Right
}

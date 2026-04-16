using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies a simple top or bottom position.
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<TopOrBottom>))]
public enum TopOrBottom
{
	/// <summary>Positioned at the top.</summary>
	Top,
	/// <summary>Positioned at the bottom.</summary>
	Bottom
}

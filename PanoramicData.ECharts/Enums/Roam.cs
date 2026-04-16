using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the roaming (pan/zoom) behavior for geographic and graph charts.
/// See https://echarts.apache.org/en/option.html#series-map.roam
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverterWithBoolean<Roam>))]
public enum Roam
{
	/// <summary>Both scaling and panning are enabled. Equivalent to <c>true</c>.</summary>
	True,
	/// <summary>Roaming is disabled. Equivalent to <c>false</c>.</summary>
	False,
	/// <summary>Only scaling (zoom) is enabled.</summary>
	Scale,
	/// <summary>Only panning (drag to move) is enabled.</summary>
	Move
}

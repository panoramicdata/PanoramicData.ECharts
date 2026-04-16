using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies where step line inflection points occur in a line series.
/// See https://echarts.apache.org/en/option.html#series-line.step
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverterWithBoolean<Step>))]
public enum Step
{
	/// <summary>Step lines are enabled (inflection at midpoint). Equivalent to <c>true</c>.</summary>
	True,
	/// <summary>Step lines are disabled; normal smooth/straight lines are drawn. Equivalent to <c>false</c>.</summary>
	False,
	/// <summary>The step inflection occurs at the start of each segment.</summary>
	Start,
	/// <summary>The step inflection occurs at the midpoint of each segment.</summary>
	Middle,
	/// <summary>The step inflection occurs at the end of each segment.</summary>
	End
}
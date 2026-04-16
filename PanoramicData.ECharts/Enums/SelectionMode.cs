using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the data selection mode for chart series.
/// See https://echarts.apache.org/en/option.html#series-bar.selectedMode
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverterWithBoolean<SelectionMode>))]
public enum SelectionMode
{
	/// <summary>Selection is enabled. Equivalent to <c>true</c>.</summary>
	True,
	/// <summary>Selection is disabled. Equivalent to <c>false</c>.</summary>
	False,
	/// <summary>Only one data item can be selected at a time.</summary>
	Single,
	/// <summary>Multiple data items can be selected simultaneously.</summary>
	Multiple
}
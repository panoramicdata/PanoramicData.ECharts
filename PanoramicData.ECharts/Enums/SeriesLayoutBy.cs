using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies whether dataset rows or columns represent individual series.
/// See https://echarts.apache.org/en/option.html#series-bar.seriesLayoutBy
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<SeriesLayoutBy>))]
public enum SeriesLayoutBy
{
	/// <summary>Each column in the dataset represents a series.</summary>
	Column,
	/// <summary>Each row in the dataset represents a series.</summary>
	Row
}
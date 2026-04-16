using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies predefined mark point types for automatically locating notable data points.
/// See https://echarts.apache.org/en/option.html#series-line.markPoint.data.type
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<MarkPointType>))]
public enum MarkPointType
{
	/// <summary>Places a mark point at the minimum value in the series.</summary>
	Min,
	/// <summary>Places a mark point at the maximum value in the series.</summary>
	Max,
	/// <summary>Places a mark point at the average value of the series.</summary>
	Average
}

using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the stacking strategy for bar or line chart series.
/// See https://echarts.apache.org/en/option.html#series-bar.stackStrategy
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<StackStrategy>))]
public enum StackStrategy
{
	/// <summary>Only stacks values with the same sign (positives with positives, negatives with negatives).</summary>
	SameSign,
	/// <summary>Stacks all values regardless of sign.</summary>
	All,
	/// <summary>Only stacks positive values; negative values are treated as zero.</summary>
	Positive,
	/// <summary>Only stacks negative values; positive values are treated as zero.</summary>
	Negative
}

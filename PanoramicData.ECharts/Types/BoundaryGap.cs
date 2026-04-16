using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents the boundary gap between the axis and the first/last data point on the axis.
/// Can be a boolean (enable/disable gap) or a pair of min/max gap values as numbers or percentages.
/// See https://echarts.apache.org/en/option.html#xAxis.boundaryGap
/// </summary>
[JsonConverter(typeof(BoundaryGapConverter))]
public class BoundaryGap
{
	/// <summary>Creates a <see cref="BoundaryGap"/> with a boolean value to enable or disable boundary gaps.</summary>
	/// <param name="b"><c>true</c> to add a gap; <c>false</c> to remove the boundary gap.</param>
	public BoundaryGap(bool b)
	{
		Bool = b;
	}

	/// <summary>Creates a <see cref="BoundaryGap"/> with explicit min and max gap values.</summary>
	/// <param name="min">The gap at the minimum side of the axis (number or percentage string).</param>
	/// <param name="max">The gap at the maximum side of the axis (number or percentage string).</param>
	public BoundaryGap(NumberOrString min, NumberOrString max)
	{
		Min = min;
		Max = max;
	}

	/// <summary>Gets the boolean boundary-gap setting, or <c>null</c> when explicit min and max values are used.</summary>
	public bool? Bool { get; }

	/// <summary>Gets the minimum-side gap value, or <c>null</c> if a boolean value is set.</summary>
	public NumberOrString? Min { get; }
	/// <summary>Gets the maximum-side gap value, or <c>null</c> if a boolean value is set.</summary>
	public NumberOrString? Max { get; }

	/// <summary>Implicitly converts a bool to a <see cref="BoundaryGap"/>.</summary>
	public static implicit operator BoundaryGap(bool b)
	{
		return new BoundaryGap(b);
	}
}

/// <summary>JSON converter for <see cref="BoundaryGap"/> that serializes to a boolean or a two-element array.</summary>
public class BoundaryGapConverter : JsonConverter<BoundaryGap>
{
	/// <inheritdoc/>
	public override BoundaryGap Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for BoundaryGap.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, BoundaryGap value, JsonSerializerOptions options)
	{
		if (value.Bool != null)
		{
			writer.WriteBooleanValue(value.Bool.Value);
		}
		else if (value.Min != null && value.Max != null)
		{
			writer.WriteStartArray();
			NumberOrStringConverter.Instance.Write(writer, value.Min, options);
			NumberOrStringConverter.Instance.Write(writer, value.Max, options);
			writer.WriteEndArray();
		}
	}
}
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents the radius of a circular chart element (e.g., Pie, Gauge).
/// Can be a single outside radius value, or a pair specifying the inside and outside radii (for donut charts).
/// Values can be absolute numbers or percentages of the chart container's smaller dimension.
/// See https://echarts.apache.org/en/option.html#series-pie.radius
/// </summary>
[JsonConverter(typeof(CircleRadiusConverter))]
public class CircleRadius
{
	/// <summary>Creates a <see cref="CircleRadius"/> with only an outside radius (full disc).</summary>
	/// <param name="outsideRadius">The outside radius value (number or percentage string).</param>
	public CircleRadius(NumberOrString outsideRadius)
	{
		OutsideRadius = outsideRadius;
	}

	/// <summary>Creates a <see cref="CircleRadius"/> with both inside and outside radii (donut/ring shape).</summary>
	/// <param name="insideRadius">The inside radius value (number or percentage string).</param>
	/// <param name="outsideRadius">The outside radius value (number or percentage string).</param>
	public CircleRadius(NumberOrString insideRadius, NumberOrString outsideRadius)
	{
		InsideRadius = insideRadius;
		OutsideRadius = outsideRadius;
	}

	/// <summary>Gets the outside radius value.</summary>
	public NumberOrString OutsideRadius { get; }
	/// <summary>Gets the inside radius value, or <c>null</c> if only an outside radius is defined.</summary>
	public NumberOrString? InsideRadius { get; }
}

/// <summary>JSON converter for <see cref="CircleRadius"/> that serializes to a single value or a two-element array [insideRadius, outsideRadius].</summary>
public class CircleRadiusConverter : JsonConverter<CircleRadius>
{
	/// <inheritdoc/>
	public override CircleRadius Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for CircleRadius.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, CircleRadius value, JsonSerializerOptions options)
	{
		if (value.InsideRadius == null)
		{
			NumberOrStringConverter.Instance.Write(writer, value.OutsideRadius, options);
		}
		else
		{
			writer.WriteStartArray();
			NumberOrStringConverter.Instance.Write(writer, value.InsideRadius, options);
			NumberOrStringConverter.Instance.Write(writer, value.OutsideRadius, options);
			writer.WriteEndArray();
		}
	}
}
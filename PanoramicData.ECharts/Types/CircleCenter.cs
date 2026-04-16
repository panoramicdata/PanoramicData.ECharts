using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents the center position of a circular chart element (e.g., Pie, Radar) as horizontal and vertical coordinates.
/// Values can be absolute numbers or percentages relative to the container size.
/// See https://echarts.apache.org/en/option.html#series-pie.center
/// </summary>
[JsonConverter(typeof(CircleCenterConverter))]
public class CircleCenter
{
	/// <summary>Creates a <see cref="CircleCenter"/> with horizontal and vertical position values.</summary>
	/// <param name="horizontal">The horizontal center position (number or percentage string).</param>
	/// <param name="vertical">The vertical center position (number or percentage string).</param>
	public CircleCenter(NumberOrString horizontal, NumberOrString vertical)
	{
		Horizontal = horizontal;
		Vertical = vertical;
	}

	/// <summary>Gets the horizontal center position.</summary>
	public NumberOrString Horizontal { get; }
	/// <summary>Gets the vertical center position.</summary>
	public NumberOrString Vertical { get; }
}

public class CircleCenterConverter : JsonConverter<CircleCenter>
{
	/// <inheritdoc/>
	public override CircleCenter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for CircleCenter.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, CircleCenter value, JsonSerializerOptions options)
	{
		writer.WriteStartArray();
		NumberOrStringConverter.Instance.Write(writer, value.Horizontal, options);
		NumberOrStringConverter.Instance.Write(writer, value.Vertical, options);
		writer.WriteEndArray();
	}
}
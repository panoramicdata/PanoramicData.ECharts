using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents CSS-style padding that can be specified as a single value (all sides), two values (top/bottom and left/right), or four individual values.
/// See https://echarts.apache.org/en/option.html#legend.padding
/// </summary>
[JsonConverter(typeof(PaddingConverter))]
public class Padding
{
	/// <summary>Creates a <see cref="Padding"/> with the same value for all four sides.</summary>
	/// <param name="all">The padding value applied to all sides.</param>
	public Padding(double all)
	{
		Top = Right = Bottom = Left = all;
	}

	/// <summary>Creates a <see cref="Padding"/> with separate top/bottom and left/right values.</summary>
	/// <param name="topBottom">The padding for the top and bottom sides.</param>
	/// <param name="leftRight">The padding for the left and right sides.</param>
	public Padding(double topBottom, double leftRight)
	{
		Top = Bottom = topBottom;
		Right = Left = leftRight;
	}

	/// <summary>Creates a <see cref="Padding"/> with individual values for each side.</summary>
	/// <param name="top">The top padding.</param>
	/// <param name="right">The right padding.</param>
	/// <param name="bottom">The bottom padding.</param>
	/// <param name="left">The left padding.</param>
	public Padding(double top, double right, double bottom, double left)
	{
		Top = top;
		Right = right;
		Bottom = bottom;
		Left = left;
	}

	/// <summary>Gets the top padding value.</summary>
	public double Top { get; }
	/// <summary>Gets the right padding value.</summary>
	public double Right { get; }
	/// <summary>Gets the bottom padding value.</summary>
	public double Bottom { get; }
	/// <summary>Gets the left padding value.</summary>
	public double Left { get; }
}

/// <summary>JSON converter for <see cref="Padding"/> that serializes to a single number or a 4-element array.</summary>
public class PaddingConverter : JsonConverter<Padding>
{
	/// <inheritdoc/>
	public override Padding Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for Padding.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, Padding value, JsonSerializerOptions options)
	{
		if (value.Top == value.Right && value.Top == value.Bottom && value.Top == value.Left)
		{
			writer.WriteNumberValue(value.Top);
		}
		else
		{
			writer.WriteStartArray();
			writer.WriteNumberValue(value.Top);
			writer.WriteNumberValue(value.Right);
			writer.WriteNumberValue(value.Bottom);
			writer.WriteNumberValue(value.Left);
			writer.WriteEndArray();
		}
	}
}
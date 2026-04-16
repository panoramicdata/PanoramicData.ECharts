using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents CSS-style border radius that can be specified as a uniform value or four individual corner values.
/// See https://echarts.apache.org/en/option.html#series-bar.itemStyle.borderRadius
/// </summary>
[JsonConverter(typeof(BorderRadiusConverter))]
public class BorderRadius
{
	/// <summary>Creates a <see cref="BorderRadius"/> with the same radius for all four corners.</summary>
	/// <param name="all">The radius applied to all corners.</param>
	public BorderRadius(double all)
	{
		UpperLeft = UpperRight = BottomRight = BottomLeft = all;
	}

	/// <summary>Creates a <see cref="BorderRadius"/> with individual values for each corner.</summary>
	/// <param name="upperLeft">The upper-left corner radius.</param>
	/// <param name="upperRight">The upper-right corner radius.</param>
	/// <param name="bottomRight">The bottom-right corner radius.</param>
	/// <param name="bottomLeft">The bottom-left corner radius.</param>
	public BorderRadius(double upperLeft, double upperRight, double bottomRight, double bottomLeft)
	{
		UpperLeft = upperLeft;
		UpperRight = upperRight;
		BottomRight = bottomRight;
		BottomLeft = bottomLeft;
	}

	/// <summary>Gets the upper-left corner radius.</summary>
	public double UpperLeft { get; }
	/// <summary>Gets the upper-right corner radius.</summary>
	public double UpperRight { get; }
	/// <summary>Gets the bottom-right corner radius.</summary>
	public double BottomRight { get; }
	/// <summary>Gets the bottom-left corner radius.</summary>
	public double BottomLeft { get; }

	/// <summary>Implicitly converts a double to a <see cref="BorderRadius"/> with uniform corners.</summary>
	public static implicit operator BorderRadius(double all)
	{
		return new BorderRadius(all);
	}
}

/// <summary>JSON converter for <see cref="BorderRadius"/> that serializes to a single number or a 4-element array.</summary>
public class BorderRadiusConverter : JsonConverter<BorderRadius>
{
	/// <inheritdoc/>
	public override BorderRadius Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for BorderRadius.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, BorderRadius value, JsonSerializerOptions options)
	{
		if (value.UpperLeft == value.UpperRight && value.UpperLeft == value.BottomRight && value.UpperLeft == value.BottomLeft)
		{
			writer.WriteNumberValue(value.UpperLeft);
		}
		else
		{
			writer.WriteStartArray();
			writer.WriteNumberValue(value.UpperLeft);
			writer.WriteNumberValue(value.UpperRight);
			writer.WriteNumberValue(value.BottomRight);
			writer.WriteNumberValue(value.BottomLeft);
			writer.WriteEndArray();
		}
	}
}
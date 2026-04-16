using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Line type
/// See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineDashOffset
/// </summary>
[JsonConverter(typeof(LineTypeConverter))]
public class LineType
{
	/// <summary>Creates a <see cref="LineType"/> from a single dash pattern number.</summary>
	/// <param name="number">The dash pattern number.</param>
	public LineType(double number)
	{
		Pattern = [number];
	}

	/// <summary>Creates a <see cref="LineType"/> from an array defining the dash pattern.</summary>
	/// <param name="pattern">Array of numbers defining dash and gap lengths.</param>
	public LineType(double[] pattern)
	{
		Pattern = pattern;
	}

	/// <summary>Creates a <see cref="LineType"/> from a named line style.</summary>
	/// <param name="style">The named line style to use.</param>
	public LineType(LineTypeStyle style)
	{
		Style = style;
	}

	/// <summary>Gets the dash pattern array, if this line type is defined by a pattern.</summary>
	public double[]? Pattern { get; }
	/// <summary>Gets the named line style, if this line type is defined by a style name.</summary>
	public LineTypeStyle? Style { get; }


	/// <summary>Implicitly converts a number to a <see cref="LineType"/> with a single-number pattern.</summary>
	public static implicit operator LineType(double number)
	{
		return new LineType(number);
	}

	/// <summary>Implicitly converts a number array to a <see cref="LineType"/> with a pattern.</summary>
	public static implicit operator LineType(double[] numbers)
	{
		return new LineType(numbers);
	}

	/// <summary>Implicitly converts a <see cref="LineTypeStyle"/> to a <see cref="LineType"/>.</summary>
	public static implicit operator LineType(LineTypeStyle type)
	{
		return new LineType(type);
	}
}

/// <summary>Named line style types for <see cref="LineType"/>.</summary>
public enum LineTypeStyle
{
	/// <summary>A solid continuous line.</summary>
	Solid,
	/// <summary>A dashed line.</summary>
	Dashed,
	/// <summary>A dotted line.</summary>
	Dotted
}

/// <summary>JSON converter for <see cref="LineType"/> that serializes to string, number, or array.</summary>
public class LineTypeConverter : JsonConverter<LineType>
{
	/// <inheritdoc/>
	public override LineType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for LineType.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, LineType value, JsonSerializerOptions options)
	{
		if (value.Style != null)
		{
			writer.WriteStringValue(value.Style!.ToString()!.ToLower());
		}
		else if (value.Pattern != null)
		{
			if (value.Pattern.Length == 1)
			{
				writer.WriteNumberValue(value.Pattern[0]);
			}
			else
			{
				writer.WriteStartArray();
				foreach (var val in value.Pattern)
				{
					writer.WriteNumberValue(val);
				}

				writer.WriteEndArray();
			}
		}
	}
}
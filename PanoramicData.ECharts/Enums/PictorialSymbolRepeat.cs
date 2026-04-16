using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies whether and how a pictorial bar symbol should be repeated along the bar.
/// See https://echarts.apache.org/en/option.html#series-pictorialBar.symbolRepeat
/// </summary>
[JsonConverter(typeof(PictorialSymbolRepeatConverter))]
public class PictorialSymbolRepeat
{
	/// <summary>Creates a <see cref="PictorialSymbolRepeat"/> with a fixed number of symbol repetitions.</summary>
	/// <param name="number">The number of times the symbol should be repeated.</param>
	public PictorialSymbolRepeat(double number)
	{
		Number = number;
	}

	/// <summary>Creates a <see cref="PictorialSymbolRepeat"/> from a named repeat type.</summary>
	/// <param name="repeatType">The named repeat type.</param>
	public PictorialSymbolRepeat(PictorialSymbolRepeatType repeatType)
	{
		RepeatType = repeatType;
	}

	/// <summary>Gets the fixed repeat count, if specified as a number.</summary>
	public double? Number { get; }
	/// <summary>Gets the named repeat type, if specified.</summary>
	public PictorialSymbolRepeatType? RepeatType { get; }

	/// <summary>Implicitly converts a number to a <see cref="PictorialSymbolRepeat"/>.</summary>
	public static implicit operator PictorialSymbolRepeat(double number)
	{
		return new PictorialSymbolRepeat(number);
	}

	/// <summary>Implicitly converts a <see cref="PictorialSymbolRepeatType"/> to a <see cref="PictorialSymbolRepeat"/>.</summary>
	public static implicit operator PictorialSymbolRepeat(PictorialSymbolRepeatType type)
	{
		return new PictorialSymbolRepeat(type);
	}
}

/// <summary>Named repeat modes for <see cref="PictorialSymbolRepeat"/>.</summary>
public enum PictorialSymbolRepeatType
{
	/// <summary>The symbol is repeated to fill the bar. Equivalent to <c>true</c>.</summary>
	True,
	/// <summary>The symbol is not repeated (single symbol). Equivalent to <c>false</c>.</summary>
	False,
	/// <summary>The symbol is repeated using a fixed spacing regardless of bar length.</summary>
	Fixed
}

/// <summary>JSON converter for <see cref="PictorialSymbolRepeat"/> that handles boolean, string, and number values.</summary>
public class PictorialSymbolRepeatConverter : JsonConverter<PictorialSymbolRepeat>
{
	/// <inheritdoc/>
	public override PictorialSymbolRepeat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for PictorialSymbolRepeat.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, PictorialSymbolRepeat value, JsonSerializerOptions options)
	{
		if (value.RepeatType != null)
		{
			switch (value.RepeatType.Value)
			{
				case PictorialSymbolRepeatType.True:
					writer.WriteBooleanValue(true);
					break;
				case PictorialSymbolRepeatType.False:
					writer.WriteBooleanValue(false);
					break;
				default:
					writer.WriteStringValue(value.RepeatType!.ToString()!.ToLower());
					break;
			}
		}
		else if (value.Number != null)
		{
			writer.WriteNumberValue(value.Number.Value);
		}
	}
}
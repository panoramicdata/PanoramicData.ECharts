using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies arrow symbols to display at the ends of axis lines.
/// Supports a single type or an array of two types (for the two ends).
/// See https://echarts.apache.org/en/option.html#xAxis.axisLine.symbol
/// </summary>
[JsonConverter(typeof(AxisSymbolConverter))]
public class AxisSymbol
{
	/// <summary>Creates an <see cref="AxisSymbol"/> with symbols for both ends.</summary>
	/// <param name="types">Array of symbol types for each end of the axis.</param>
	public AxisSymbol(AxisSymbolType[] types)
	{
		Types = types;
	}

	/// <summary>Creates an <see cref="AxisSymbol"/> with the same symbol for both ends.</summary>
	/// <param name="type">The symbol type to use for both ends.</param>
	public AxisSymbol(AxisSymbolType type)
	{
		Types = [type];
	}

	/// <summary>Gets the array of axis symbol types.</summary>
	public AxisSymbolType[] Types { get; }
}

/// <summary>Symbol types available for axis line ends.</summary>
public enum AxisSymbolType
{
	/// <summary>No symbol at the axis end.</summary>
	None,
	/// <summary>An arrow symbol at the axis end.</summary>
	Arrow
}

/// <summary>JSON converter for <see cref="AxisSymbol"/> that serializes to a string or string array.</summary>
public class AxisSymbolConverter : JsonConverter<AxisSymbol>
{
	/// <inheritdoc/>
	public override AxisSymbol Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for AxisSymbol.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, AxisSymbol value, JsonSerializerOptions options)
	{
		if (value.Types.Length == 1)
		{
			writer.WriteStringValue(value.Types[0].ToString()!.ToLower());
		}
		else
		{
			writer.WriteStartArray();
			foreach (var val in value.Types)
			{
				writer.WriteStringValue(val.ToString().ToLower());
			}

			writer.WriteEndArray();
		}
	}
}
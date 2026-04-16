using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents the arrow symbols at the start and end of an axis line.
/// Can be set to the same symbol for both ends, or different symbols for start and end.
/// See https://echarts.apache.org/en/option.html#xAxis.axisLine.symbol
/// </summary>
[JsonConverter(typeof(AxisLineSymbolConverter))]
public class AxisLineSymbol
{
	/// <summary>Creates an <see cref="AxisLineSymbol"/> using the same symbol for both ends of the axis line.</summary>
	/// <param name="both">The symbol to use for both the start and end of the axis line.</param>
	public AxisLineSymbol(Icon both)
	{
		Start = both;
		End = both;
		AreSame = true;
	}

	/// <summary>Creates an <see cref="AxisLineSymbol"/> with different symbols for each end of the axis line.</summary>
	/// <param name="start">The symbol at the start (min side) of the axis line.</param>
	/// <param name="end">The symbol at the end (max side) of the axis line.</param>
	public AxisLineSymbol(Icon start, Icon end)
	{
		Start = start;
		End = end;
		AreSame = false;
	}

	/// <summary>Gets the symbol at the start of the axis line.</summary>
	public Icon Start { get; }
	/// <summary>Gets the symbol at the end of the axis line.</summary>
	public Icon End { get; }

	internal bool AreSame { get; }
}

/// <summary>JSON converter for <see cref="AxisLineSymbol"/> that serializes as a single value or two-element array.</summary>
public class AxisLineSymbolConverter : JsonConverter<AxisLineSymbol>
{
	/// <inheritdoc/>
	public override AxisLineSymbol Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for AxisLineSymbol.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, AxisLineSymbol value, JsonSerializerOptions options)
	{
		if (value.AreSame)
		{
			IconConverter.Instance.Write(writer, value.Start, options);
		}
		else
		{
			writer.WriteStartArray();
			IconConverter.Instance.Write(writer, value.Start, options);
			IconConverter.Instance.Write(writer, value.End, options);
			writer.WriteEndArray();
		}
	}
}
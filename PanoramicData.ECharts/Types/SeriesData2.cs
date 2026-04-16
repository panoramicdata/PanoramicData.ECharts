using System.Text.Json.Serialization;
using System.Text.Json;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a two-element data point in an ECharts series, holding two typed values serialized as a JSON array.
/// </summary>
/// <typeparam name="T1">The type of the first data value.</typeparam>
/// <typeparam name="T2">The type of the second data value.</typeparam>
public struct SeriesData<T1, T2>
{
	/// <summary>Creates a <see cref="SeriesData{T1, T2}"/> with two values.</summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	public SeriesData(T1? value1, T2? value2)
	{
		Value1 = value1;
		Value2 = value2;
	}

	/// <summary>Gets or sets the first data value.</summary>
	public T1? Value1 { get; set; }

	/// <summary>Gets or sets the second data value.</summary>
	public T2? Value2 { get; set; }
}

/// <summary>JSON converter for <see cref="SeriesData{T1, T2}"/> that serializes as a two-element JSON array.</summary>
public class SeriesDataConverter<T1, T2> : JsonConverter<SeriesData<T1, T2>>
{
	/// <inheritdoc/>
	public override SeriesData<T1, T2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for SeriesData<T1, T2>.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, SeriesData<T1, T2> value, JsonSerializerOptions options)
	{
		writer.WriteStartArray();

		if (value.Value1 == null)
		{
			writer.WriteNullValue();
		}
		else
		{
			JsonSerializer.Serialize(writer, value.Value1, options);
		}

		if (value.Value2 == null)
		{
			writer.WriteNullValue();
		}
		else
		{
			JsonSerializer.Serialize(writer, value.Value2, options);
		}

		writer.WriteEndArray();
	}
}
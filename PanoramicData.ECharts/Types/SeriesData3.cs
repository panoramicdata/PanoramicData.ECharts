using System.Text.Json.Serialization;
using System.Text.Json;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a three-element data point in an ECharts series, holding three typed values serialized as a JSON array.
/// </summary>
/// <typeparam name="T1">The type of the first data value.</typeparam>
/// <typeparam name="T2">The type of the second data value.</typeparam>
/// <typeparam name="T3">The type of the third data value.</typeparam>
public struct SeriesData<T1, T2, T3>
{
	/// <summary>Creates a <see cref="SeriesData{T1, T2, T3}"/> with three values.</summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="value3">The third value.</param>
	public SeriesData(T1? value1, T2? value2, T3? value3)
	{
		Value1 = value1;
		Value2 = value2;
		Value3 = value3;
	}

	/// <summary>Gets or sets the first data value.</summary>
	public T1? Value1 { get; set; }

	/// <summary>Gets or sets the second data value.</summary>
	public T2? Value2 { get; set; }

	/// <summary>Gets or sets the third data value.</summary>
	public T3? Value3 { get; set; }
}

/// <summary>JSON converter for <see cref="SeriesData{T1, T2, T3}"/> that serializes as a three-element JSON array.</summary>
public class SeriesDataConverter<T1, T2, T3> : JsonConverter<SeriesData<T1, T2, T3>>
{
	/// <inheritdoc/>
	public override SeriesData<T1, T2, T3> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for SeriesData<T1, T2, T3>.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, SeriesData<T1, T2, T3> value, JsonSerializerOptions options)
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

		if (value.Value3 == null)
		{
			writer.WriteNullValue();
		}
		else
		{
			JsonSerializer.Serialize(writer, value.Value3, options);
		}

		writer.WriteEndArray();
	}
}
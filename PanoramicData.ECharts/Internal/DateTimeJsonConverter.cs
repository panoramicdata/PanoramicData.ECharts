using System.Text.Json.Serialization;
using System.Text.Json;

namespace PanoramicData.ECharts.Internal;

/// <summary>
/// Serializes <see cref="DateTime"/> values to ISO 8601 UTC strings for ECharts (e.g., "2024-06-15T12:00:00.000Z").
/// Local times are converted to UTC before serialization.
/// </summary>
public class DateTimeJsonConverter : JsonConverter<DateTime>
{
	/// <inheritdoc/>
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for DateTime.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
	{
		if (value.Kind == DateTimeKind.Local)
			value = value.ToUniversalTime();
		writer.WriteStringValue($"{value.Year:D4}-{value.Month:D2}-{value.Day:D2}T{value.Hour:D2}:{value.Minute:D2}:{value.Second:D2}.{value.Millisecond:D3}Z");
	}
}

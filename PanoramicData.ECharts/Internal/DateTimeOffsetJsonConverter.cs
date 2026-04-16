using System.Text.Json.Serialization;
using System.Text.Json;

namespace PanoramicData.ECharts.Internal;

/// <summary>
/// Serializes <see cref="DateTimeOffset"/> values to ISO 8601 UTC strings for ECharts (e.g., "2024-06-15T12:00:00.000Z").
/// The value is converted to UTC before serialization.
/// </summary>
public class DateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
{
	/// <inheritdoc/>
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for DateTimeOffset.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
	{
		var dt = value.ToUniversalTime().DateTime;
		writer.WriteStringValue($"{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2}T{dt.Hour:D2}:{dt.Minute:D2}:{dt.Second:D2}.{dt.Millisecond:D3}Z");
	}
}

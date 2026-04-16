using System.Text.Json.Serialization;
using System.Text.Json;

namespace PanoramicData.ECharts.Internal;

/// <summary>
/// Serializes <see cref="DateOnly"/> values to ISO 8601 date strings for ECharts (e.g., "2024-06-15").
/// </summary>
public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
	/// <inheritdoc/>
	public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for DateOnly.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) => writer.WriteStringValue($"{value.Year:D4}-{value.Month:D2}-{value.Day:D2}");
}

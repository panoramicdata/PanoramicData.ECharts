using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a reference to a previously registered <see cref="ExternalDataSource"/>, used where ECharts expects fetched data.
/// Serializes as the marker object <c>{"$pdDs":"fetchId","path":"..."}</c>, which the browser resolves to the fetched data.
/// </summary>
public class ExternalDataSourceRef
{
	/// <summary>Creates an <see cref="ExternalDataSourceRef"/> from an <see cref="ExternalDataSource"/> instance.</summary>
	/// <param name="dataSource">The external data source to reference.</param>
	/// <param name="path">Optional dot-separated path to a property within the fetched data object, such as <c>nodes</c> or <c>items[0].values</c>. Only own properties are followed; it is not evaluated as JavaScript.</param>
	public ExternalDataSourceRef(ExternalDataSource dataSource, string? path = null)
	{
		FetchId = dataSource.FetchId;
		Path = path;
	}

	/// <summary>Creates an <see cref="ExternalDataSourceRef"/> from a fetch identifier string.</summary>
	/// <param name="fetchId">The unique identifier of the external data source.</param>
	/// <param name="path">Optional dot-separated path to a property within the fetched data object, such as <c>nodes</c> or <c>items[0].values</c>. Only own properties are followed; it is not evaluated as JavaScript.</param>
	public ExternalDataSourceRef(string fetchId, string? path = null)
	{
		FetchId = fetchId;
		Path = path;
	}

	/// <summary>Gets the unique identifier of the referenced external data source.</summary>
	public string FetchId { get; }
	/// <summary>Gets the optional data path within the fetched object, or <c>null</c> if the entire object is used.</summary>
	public string? Path { get; }
}

/// <summary>JSON converter for <see cref="ExternalDataSourceRef"/>: writes a marker object that the browser resolves to the fetched data.</summary>
public class ExternalDataSourceRefConverter : JsonConverter<ExternalDataSourceRef>
{
	internal const string MarkerProperty = "$pdDs";

	internal ExternalDataSourceRefConverter()
	{
	}

	/// <inheritdoc/>
	public override ExternalDataSourceRef Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for ExternalDataSourceRef.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, ExternalDataSourceRef value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteString(MarkerProperty, value.FetchId);
		if (value.Path != null)
		{
			writer.WriteString("path", value.Path);
		}

		writer.WriteEndObject();
	}
}
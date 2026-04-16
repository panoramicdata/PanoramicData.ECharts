using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a reference to a previously registered <see cref="ExternalDataSource"/>, used where ECharts expects fetched data.
/// Serializes as a JavaScript expression that retrieves the data by its fetch ID.
/// </summary>
public class ExternalDataSourceRef
{
	/// <summary>Creates an <see cref="ExternalDataSourceRef"/> from an <see cref="ExternalDataSource"/> instance.</summary>
	/// <param name="dataSource">The external data source to reference.</param>
	/// <param name="path">Optional dot-separated path to a property within the fetched data object.</param>
	public ExternalDataSourceRef(ExternalDataSource dataSource, string? path = null)
	{
		FetchId = dataSource.FetchId;
		Path = path;
	}

	/// <summary>Creates an <see cref="ExternalDataSourceRef"/> from a fetch identifier string.</summary>
	/// <param name="fetchId">The unique identifier of the external data source.</param>
	/// <param name="path">Optional dot-separated path to a property within the fetched data object.</param>
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

/// <summary>JSON converter for <see cref="ExternalDataSourceRef"/> that serializes as a raw JavaScript expression to retrieve data from the browser cache.</summary>
public class ExternalDataSourceRefConverter : JsonConverter<ExternalDataSourceRef>
{
	internal ExternalDataSourceRefConverter()
	{
	}

	/// <inheritdoc/>
	public override ExternalDataSourceRef Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for ExternalDataSourceRef.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, ExternalDataSourceRef value, JsonSerializerOptions options)
	{
	string raw = $"window.panoramicDataECharts.getDataSource('{value.FetchId}')";
	if (value.Path != null)
	{
		if (!value.Path.StartsWith('.'))
		{
			raw += '.';
		}

		raw += value.Path;
	}		writer.WriteRawValue(raw, skipInputValidation: true);
	}
}
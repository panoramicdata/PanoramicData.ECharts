using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies how the content of an external data source is parsed when fetched.
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<ExternalDataFetchAs>))]
public enum ExternalDataFetchAs
{
	/// <summary>The fetched content is parsed as JSON data.</summary>
	Json,
	/// <summary>The fetched content is treated as a raw string.</summary>
	String
}

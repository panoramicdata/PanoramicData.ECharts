namespace PanoramicData.ECharts;

/// <summary>
/// Marker interface for ECharts visual map configurations.
/// </summary>
public interface IVisualMap
{
	/// <summary>Gets the visual map type identifier used by ECharts.</summary>
	string Type { get; }
}

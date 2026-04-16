namespace PanoramicData.ECharts;

/// <summary>
/// Marker interface for ECharts data zoom configurations.
/// </summary>
public interface IDataZoom
{
	/// <summary>Gets the data zoom type identifier used by ECharts.</summary>
	string Type { get; }
}

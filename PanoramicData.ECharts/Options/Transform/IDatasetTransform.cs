namespace PanoramicData.ECharts;

/// <summary>
/// Marker interface for ECharts dataset transform configurations.
/// </summary>
public interface IDatasetTransform
{
	/// <summary>Gets the dataset transform type identifier used by ECharts.</summary>
	string Type { get; }
}

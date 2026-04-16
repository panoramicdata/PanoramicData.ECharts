namespace PanoramicData.ECharts;

/// <summary>
/// Marker interface for ECharts graphic color values, such as linear and radial gradients.
/// </summary>
public interface IGraphicColor
{
	/// <summary>Gets the type identifier used by ECharts (e.g., "linear" or "radial").</summary>
	string Type { get; }
}

namespace PanoramicData.ECharts;

/// <summary>Marker interface for ECharts series configurations.</summary>
public interface ISeries
{
	/// <summary>Gets the series type identifier used by ECharts.</summary>
	string Type { get; }
}

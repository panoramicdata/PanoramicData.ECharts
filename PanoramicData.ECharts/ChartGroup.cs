namespace PanoramicData.ECharts;

/// <summary>
/// Allows multiple charts to be grouped together, allowing easy bulk update calls
/// </summary>
public class ChartGroup
{
	private readonly List<EChartBase> charts = [];

	internal void Add(EChartBase chart) => charts.Add(chart);

	internal void Remove(EChartBase chart) => charts.Remove(chart);

	/// <summary>Triggers an update on all charts in the group.</summary>
	/// <param name="executeDataLoader">Whether to re-execute data loaders on each chart.</param>
	public async Task UpdateAsync(bool executeDataLoader = true)
	{
		foreach (var chart in charts)
		{
			await chart.UpdateAsync(executeDataLoader);
		}
	}
}

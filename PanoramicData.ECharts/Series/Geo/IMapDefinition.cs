namespace PanoramicData.ECharts;

/// <summary>
/// See https://echarts.apache.org/en/api.html#echarts.registerMap
/// </summary>
public interface IMapDefinition
{
	/// <summary>Gets the registered map definition type identifier.</summary>
	string Type { get; }

	/// <summary>Gets or sets the registered map name.</summary>
	string Name { get; set; }
}

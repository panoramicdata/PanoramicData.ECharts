using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// See https://echarts.apache.org/en/api.html#echarts.registerMap
/// </summary>
public class SvgMapDefinition : IMapDefinition
{
	/// <summary>Creates an SVG map definition from a raw SVG string.</summary>
	public SvgMapDefinition(string name, string svgData)
	{
		Name = name;
		Svg = svgData;
	}

	/// <summary>Creates an SVG map definition from a parsed SVG object.</summary>
	public SvgMapDefinition(string name, object svgData)
	{
		Name = name;
		Svg = svgData;
	}

	/// <summary>Creates an SVG map definition from an external data source.</summary>
	public SvgMapDefinition(string name, ExternalDataSource dataSource)
	{
		Name = name;
		Svg = new ExternalDataSourceRef(dataSource);
	}

	/// <summary>Creates an SVG map definition from an external data source reference.</summary>
	public SvgMapDefinition(string name, ExternalDataSourceRef dataSource)
	{
		Name = name;
		Svg = dataSource;
	}

	/// <summary>Gets the registered map definition type identifier.</summary>
	public string Type => "svg";

	/// <summary>
	/// Map name, referring to map value set in geo component or map.
	/// </summary>
	[JsonPropertyName("mapName")]
	public string Name { get; set; }

	/// <summary>
	///  Data in SVG format.
	///  Can be a SVG string or a parsed SVG DOM object.
	///  See more info in SVG Base Map: https://echarts.apache.org/en/tutorial.html#SVG%20Base%20Map%20in%20Geo%20Coords%20and%20Map%20SeriesSVG%20Base%20Map.
	/// </summary>
	[JsonPropertyName("svg")]
	public object Svg { get; set; }
}

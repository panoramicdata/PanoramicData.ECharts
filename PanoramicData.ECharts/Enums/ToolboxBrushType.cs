using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// See https://echarts.apache.org/en/option.html#toolbox.feature.brush.type
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<ToolboxBrushType>))]
public enum ToolboxBrushType
{
	/// <summary>Rectangle brush selection.</summary>
	Rect,
	/// <summary>Polygon brush selection.</summary>
	Polygon,
	/// <summary>Horizontal line brush selection.</summary>
	LineX,
	/// <summary>Vertical line brush selection.</summary>
	LineY,
	/// <summary>Keeps the current brush selection.</summary>
	Keep,
	/// <summary>Clears the current brush selection.</summary>
	Clear
}
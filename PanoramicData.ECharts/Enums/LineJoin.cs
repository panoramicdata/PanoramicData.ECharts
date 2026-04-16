using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the shape used at the corner where two lines meet.
/// See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineJoin
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<LineJoin>))]
public enum LineJoin
{
	/// <summary>Joins lines with a diagonal flat edge, cutting off sharp corners.</summary>
	Bevel,
	/// <summary>Joins lines with a rounded corner.</summary>
	Round,
	/// <summary>Joins lines with a sharp pointed corner (default). May be clipped by miterLimit.</summary>
	Miter
}
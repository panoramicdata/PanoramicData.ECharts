using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Abstract base class for ECharts gradient color types (linear and radial).
/// See https://github.com/ecomfe/zrender/blob/master/src/graphic/Gradient.ts
/// </summary>
public abstract class Gradient : IGraphicColor
{
	/// <summary>Gets the gradient type string ("linear" or "radial").</summary>
	[JsonPropertyName("type")]
	public abstract string Type { get; }

	/// <summary>Gets or sets an optional identifier for the gradient.</summary>
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	/// <summary>Gets or sets the color stops that define the gradient colors and positions.</summary>
	[JsonPropertyName("colorStops")]
	public GradientColorStop[]? ColorStops { get; set; }

	/// <summary>Gets or sets whether the gradient coordinates are in global (pixel) space. When <c>false</c>, coordinates are relative to the bounding box.</summary>
	[JsonPropertyName("global")]
	public bool? Global { get; set; }
}

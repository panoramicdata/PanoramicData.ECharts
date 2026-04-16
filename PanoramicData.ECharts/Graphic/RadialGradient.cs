using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a radial gradient color, emanating from a center point outward to a given radius.
/// See https://github.com/ecomfe/zrender/blob/master/src/graphic/RadialGradient.ts
/// </summary>
public class RadialGradient : Gradient
{
	/// <inheritdoc/>
	public override string Type => "radial";

	/// <summary>Creates a <see cref="RadialGradient"/> with a center point, radius, and optional color stops.</summary>
	/// <param name="x">The x-coordinate of the gradient center (0-1 or pixel, depending on <paramref name="global"/>).</param>
	/// <param name="y">The y-coordinate of the gradient center.</param>
	/// <param name="r">The radius of the gradient.</param>
	/// <param name="colorStops">The color stops defining gradient colors and positions.</param>
	/// <param name="global">Whether coordinates are in global pixel space.</param>
	public RadialGradient(double x, double y, double r, GradientColorStop[]? colorStops = null, bool? global = null)
	{
		X = x;
		Y = y;
		R = r;

		ColorStops = colorStops;
		Global = global;
	}

	/// <summary>Gets or sets the x-coordinate of the gradient center.</summary>
	[JsonPropertyName("x")]
	public double X { get; set; }

	/// <summary>Gets or sets the y-coordinate of the gradient center.</summary>
	[JsonPropertyName("y")]
	public double Y { get; set; }

	/// <summary>Gets or sets the radius of the gradient.</summary>
	[JsonPropertyName("r")]
	public double R { get; set; }
}

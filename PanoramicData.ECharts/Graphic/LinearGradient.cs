using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents a linear gradient color, transitioning along a straight line from (x,y) to (x2,y2).
/// See https://github.com/ecomfe/zrender/blob/master/src/graphic/LinearGradient.ts
/// </summary>
public class LinearGradient : Gradient
{
	/// <inheritdoc/>
	public override string Type => "linear";

	/// <summary>Creates a <see cref="LinearGradient"/> with start/end coordinates and optional color stops.</summary>
	/// <param name="x">The x-coordinate of the gradient start point (0-1 or pixel, depending on <paramref name="global"/>).</param>
	/// <param name="y">The y-coordinate of the gradient start point.</param>
	/// <param name="x2">The x-coordinate of the gradient end point.</param>
	/// <param name="y2">The y-coordinate of the gradient end point.</param>
	/// <param name="colorStops">The color stops defining gradient colors and positions.</param>
	/// <param name="global">Whether coordinates are in global pixel space.</param>
	public LinearGradient(double x, double y, double x2, double y2, GradientColorStop[]? colorStops = null, bool? global = null)
	{
		X = x;
		Y = y;
		X2 = x2;
		Y2 = y2;

		ColorStops = colorStops;
		Global = global;
	}

	/// <summary>Gets or sets the x-coordinate of the gradient start point.</summary>
	[JsonPropertyName("x")]
	public double X { get; set; }

	/// <summary>Gets or sets the y-coordinate of the gradient start point.</summary>
	[JsonPropertyName("y")]
	public double Y { get; set; }

	/// <summary>Gets or sets the x-coordinate of the gradient end point.</summary>
	[JsonPropertyName("x2")]
	public double X2 { get; set; }

	/// <summary>Gets or sets the y-coordinate of the gradient end point.</summary>
	[JsonPropertyName("y2")]
	public double Y2 { get; set; }
}

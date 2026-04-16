namespace PanoramicData.ECharts;

/// <summary>
/// Represents a single color stop within a gradient, specifying a position and color.
/// See https://github.com/ecomfe/zrender/blob/master/src/graphic/Gradient.ts
/// </summary>
public class GradientColorStop
{
	/// <summary>Creates a <see cref="GradientColorStop"/> with a position and color.</summary>
	/// <param name="offset">The stop position along the gradient axis, from 0 (start) to 1 (end).</param>
	/// <param name="color">The color at this stop.</param>
	public GradientColorStop(double offset, Color color)
	{
		Offset = offset;
		Color = color;
	}

	/// <summary>Gets or sets the stop position from 0 (start) to 1 (end).</summary>
	public double Offset { get; set; }
	/// <summary>Gets or sets the color at this stop.</summary>
	public Color Color { get; set; }
}

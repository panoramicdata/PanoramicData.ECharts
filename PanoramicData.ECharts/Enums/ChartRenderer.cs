namespace PanoramicData.ECharts;

/// <summary>
/// See https://echarts.apache.org/handbook/en/best-practices/canvas-vs-svg/
/// </summary>
public enum ChartRenderer
{
	/// <summary>Uses the SVG renderer. Better for large datasets that need high-fidelity vector output or accessibility.</summary>
	Svg,
	/// <summary>Uses the Canvas renderer (default). Better performance for most chart types.</summary>
	Canvas
}

/// <summary>Extension methods for <see cref="ChartRenderer"/>.</summary>
public static class ChartRendererExtensions
{
	/// <summary>Converts the <see cref="ChartRenderer"/> value to the corresponding ECharts JavaScript parameter string.</summary>
	/// <param name="renderer">The renderer to convert.</param>
	/// <returns>"svg" or "canvas".</returns>
	public static string ToJsParam(this ChartRenderer renderer) => renderer switch
	{
		ChartRenderer.Svg => "svg",
		ChartRenderer.Canvas => "canvas",
		_ => throw new ArgumentException($"Renderer '{renderer}' not supported")
	};
}
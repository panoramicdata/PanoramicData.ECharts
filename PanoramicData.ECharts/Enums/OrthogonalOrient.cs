using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the orthogonal orientation direction for tree and graph layouts.
/// The values map to ECharts direction strings: LR, RL, TB, BT.
/// See https://echarts.apache.org/en/option.html#series-tree.orient
/// </summary>
[JsonConverter(typeof(OrthogonalOrientConverter))]
public enum OrthogonalOrient
{
	/// <summary>Left to right direction (LR). Alias for <see cref="Horizontal"/>.</summary>
	LeftToRight,
	/// <summary>Right to left direction (RL).</summary>
	RightToLeft,
	/// <summary>Top to bottom direction (TB). Alias for <see cref="Vertical"/>.</summary>
	TopToBottom,
	/// <summary>Bottom to top direction (BT).</summary>
	BottomToTop,

	/// <summary>Horizontal direction (left to right). Alias for <see cref="LeftToRight"/>.</summary>
	Horizontal,
	/// <summary>Vertical direction (top to bottom). Alias for <see cref="TopToBottom"/>.</summary>
	Vertical
}

/// <summary>JSON converter for <see cref="OrthogonalOrient"/> that maps values to ECharts direction strings (LR, RL, TB, BT).</summary>
public class OrthogonalOrientConverter : JsonConverter<OrthogonalOrient>
{
	/// <inheritdoc/>
	public override OrthogonalOrient Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for OrthogonalOrient.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, OrthogonalOrient value, JsonSerializerOptions options) => writer.WriteStringValue(GetValue(value));

	private static string GetValue(OrthogonalOrient value) => value switch
	{
		OrthogonalOrient.LeftToRight or OrthogonalOrient.Horizontal => "LR",
		OrthogonalOrient.RightToLeft => "RL",
		OrthogonalOrient.TopToBottom or OrthogonalOrient.Vertical => "TB",
		OrthogonalOrient.BottomToTop => "BT",
		_ => "LR"
	};
}
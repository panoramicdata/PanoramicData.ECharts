using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/globalCompositeOperation
/// </summary>
[JsonConverter(typeof(BlendModeConverter))]
public enum BlendMode
{
	/// <summary>The source is drawn on top of the destination. Default compositing mode.</summary>
	SourceOver,
	/// <summary>The source is drawn where it overlaps the destination.</summary>
	SourceIn,
	/// <summary>The source is drawn where it does not overlap the destination.</summary>
	SourceOut,
	/// <summary>The source is drawn on top of the destination and clipped to the destination bounds.</summary>
	SourceAtop,
	/// <summary>The destination is drawn on top of the source.</summary>
	DestinationOver,
	/// <summary>The destination is drawn where it overlaps the source.</summary>
	DestinationIn,
	/// <summary>The destination is drawn where it does not overlap the source.</summary>
	DestinationOut,
	/// <summary>The destination is drawn on top of the source and clipped to the source bounds.</summary>
	DestinationAtop,
	/// <summary>Both source and destination are added together, making the result brighter.</summary>
	Lighter,
	/// <summary>Only the source is shown; the destination is ignored.</summary>
	Copy,
	/// <summary>The source and destination are combined using XOR logic, making overlaps transparent.</summary>
	Xor,
	/// <summary>Multiplies the source and destination colors, resulting in a darker color.</summary>
	Multiply,
	/// <summary>Multiplies the complement of source and destination, resulting in a lighter color.</summary>
	Screen,
	/// <summary>Combines Multiply and Screen modes depending on the destination color.</summary>
	Overlay,
	/// <summary>Retains the darker of the source and destination pixels.</summary>
	Darken,
	/// <summary>Retains the lighter of the source and destination pixels.</summary>
	Lighten,
	/// <summary>Brightens the destination to reflect the source color.</summary>
	ColorDodge,
	/// <summary>Darkens the destination to reflect the source color.</summary>
	ColorBurn,
	/// <summary>Applies Multiply or Screen depending on the source color.</summary>
	HardLight,
	/// <summary>Applies a softer version of Hard Light, similar to diffused spotlight.</summary>
	SoftLight,
	/// <summary>Subtracts the darker of the two constituent colors from the lighter one.</summary>
	Difference,
	/// <summary>Similar to Difference, but with lower contrast.</summary>
	Exclusion,
	/// <summary>Preserves the luma and chroma of the destination, while adopting the hue of the source.</summary>
	Hue,
	/// <summary>Preserves the luma and hue of the destination, while adopting the chroma of the source.</summary>
	Saturation,
	/// <summary>Preserves the luma of the destination, while adopting the hue and chroma of the source.</summary>
	Color,
	/// <summary>Preserves the hue and chroma of the destination, while adopting the luma of the source.</summary>
	Luminosity
}

/// <summary>JSON converter for <see cref="BlendMode"/> that serializes values to CSS blend mode strings.</summary>
public class BlendModeConverter : JsonConverter<BlendMode>
{
	/// <inheritdoc/>
	public override BlendMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for BlendMode.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, BlendMode value, JsonSerializerOptions options) => writer.WriteStringValue(GetValue(value));

	private static string GetValue(BlendMode value) => value switch
	{
		BlendMode.SourceOver => "source-over",
		BlendMode.SourceIn => "source-in",
		BlendMode.SourceOut => "source-out",
		BlendMode.SourceAtop => "source-atop",
		BlendMode.DestinationOver => "destination-over",
		BlendMode.DestinationIn => "destination-in",
		BlendMode.DestinationOut => "destination-out",
		BlendMode.DestinationAtop => "destination-atop",
		_ => value.ToString().ToLower()
	};
}
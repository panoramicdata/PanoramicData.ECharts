using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the weight (or boldness) of a font for chart text elements.
/// Supports both named CSS font-weight values and numeric 100-900 values.
/// See https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight
/// </summary>
[JsonConverter(typeof(FontWeightConverter))]
public enum FontWeight
{
	/// <summary>Normal font weight (equivalent to 400).</summary>
	Normal,
	/// <summary>Bold font weight (equivalent to 700).</summary>
	Bold,
	/// <summary>Bolder than the inherited font weight.</summary>
	Bolder,
	/// <summary>Lighter than the inherited font weight.</summary>
	Lighter,

	/// <summary>Thin font weight (100).</summary>
	Thin100,
	/// <summary>Extra-light font weight (200).</summary>
	ExtraLight200,
	/// <summary>Light font weight (300).</summary>
	Light300,
	/// <summary>Normal font weight (400), same as <see cref="Normal"/>.</summary>
	Normal400,
	/// <summary>Medium font weight (500).</summary>
	Medium500,
	/// <summary>Semi-bold font weight (600).</summary>
	SemiBold600,
	/// <summary>Bold font weight (700), same as <see cref="Bold"/>.</summary>
	Bold700,
	/// <summary>Extra-bold font weight (800).</summary>
	ExtraBold800,
	/// <summary>Black (heaviest) font weight (900).</summary>
	Black900
}

/// <summary>JSON converter for <see cref="FontWeight"/> that maps named and numeric weight values to their CSS equivalents.</summary>
public class FontWeightConverter : JsonConverter<FontWeight>
{
	/// <inheritdoc/>
	public override FontWeight Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for FontWeight.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, FontWeight value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case FontWeight.Normal:
				writer.WriteStringValue("normal");
				break;
			case FontWeight.Bold:
				writer.WriteStringValue("bold");
				break;
			case FontWeight.Bolder:
				writer.WriteStringValue("bolder");
				break;
			case FontWeight.Lighter:
				writer.WriteStringValue("lighter");
				break;

			case FontWeight.Thin100:
				writer.WriteStringValue("100");
				break;
			case FontWeight.ExtraLight200:
				writer.WriteStringValue("200");
				break;
			case FontWeight.Light300:
				writer.WriteStringValue("300");
				break;
			case FontWeight.Normal400:
				writer.WriteStringValue("400");
				break;
			case FontWeight.Medium500:
				writer.WriteStringValue("500");
				break;
			case FontWeight.SemiBold600:
				writer.WriteStringValue("600");
				break;
			case FontWeight.Bold700:
				writer.WriteStringValue("700");
				break;
			case FontWeight.ExtraBold800:
				writer.WriteStringValue("800");
				break;
			case FontWeight.Black900:
				writer.WriteStringValue("900");
				break;
		}

	}
}
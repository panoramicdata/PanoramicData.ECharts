using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Border type
/// See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineDashOffset
/// </summary>
[JsonConverter(typeof(SelectorConverter))]
public class Selector
{
	/// <summary>Creates a <see cref="Selector"/> that enables or disables the select-all/inverse buttons.</summary>
	/// <param name="enabled">Whether to enable the selector buttons.</param>
	public Selector(bool enabled)
	{
		Enabled = enabled;
	}

	/// <summary>Creates a <see cref="Selector"/> with specific button types.</summary>
	/// <param name="buttons">The selector button types to display.</param>
	public Selector(params SelectorType[] buttons)
	{
		Buttons = buttons;
	}

	/// <summary>Creates a <see cref="Selector"/> with custom labels for each button.</summary>
	/// <param name="buttonsWithLabel">The selector buttons with their custom label text.</param>
	public Selector(params KeyValuePair<SelectorType, string>[] buttonsWithLabel)
	{
		ButtonsWithLabel = buttonsWithLabel;
	}

	/// <summary>Gets whether the selector is enabled or disabled, if set as a boolean.</summary>
	public bool Enabled { get; }
	/// <summary>Gets the array of selector button types, if set.</summary>
	public SelectorType[]? Buttons { get; }
	/// <summary>Gets the array of selector buttons with custom labels, if set.</summary>
	public KeyValuePair<SelectorType, string>[]? ButtonsWithLabel { get; }
}

/// <summary>Types of selector buttons for the legend component.</summary>
public enum SelectorType
{
	/// <summary>A button that selects all legend items.</summary>
	All,
	/// <summary>A button that inverts the current legend selection.</summary>
	Inverse
}

/// <summary>JSON converter for <see cref="Selector"/> that serializes to boolean or array format.</summary>
public class SelectorConverter : JsonConverter<Selector>
{
	/// <inheritdoc/>
	public override Selector Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for Selector.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, Selector value, JsonSerializerOptions options)
	{
		if (value.ButtonsWithLabel != null)
		{
			writer.WriteStartArray();
			foreach (var val in value.ButtonsWithLabel)
			{
				writer.WriteStartObject();
				writer.WriteString("type", val.Key.ToString()!.ToLower());
				writer.WriteString("title", val.Value);
				writer.WriteEndObject();
			}

			writer.WriteEndArray();

		}
		else if (value.Buttons != null)
		{
			writer.WriteStartArray();
			foreach (var val in value.Buttons)
			{
				writer.WriteStringValue(val.ToString()!.ToLower());
			}

			writer.WriteEndArray();
		}
		else
		{
			writer.WriteBooleanValue(value.Enabled);
		}
	}
}
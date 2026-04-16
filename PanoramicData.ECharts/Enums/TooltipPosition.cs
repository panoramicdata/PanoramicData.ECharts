using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the position of the tooltip, which can be a named position type, a coordinate array, or a JavaScript function.
/// See https://echarts.apache.org/en/option.html#tooltip.position
/// </summary>
[JsonConverter(typeof(TooltipPositionConverter))]
public class TooltipPosition
{
	/// <summary>Creates a <see cref="TooltipPosition"/> from a named position type.</summary>
	/// <param name="type">The named position type.</param>
	public TooltipPosition(TooltipPositionType type)
	{
		Type = type;
	}

	/// <summary>Creates a <see cref="TooltipPosition"/> from a JavaScript function.</summary>
	/// <param name="function">A JavaScript function that returns the tooltip position.</param>
	public TooltipPosition(JavascriptFunction function)
	{
		Function = function;
	}

	/// <summary>Creates a <see cref="TooltipPosition"/> from a coordinate array (x, y).</summary>
	/// <param name="position">Array of number-or-string values representing the position coordinates.</param>
	public TooltipPosition(params NumberOrString[] position)
	{
		Position = position;
	}

	/// <summary>Gets the named position type, if set.</summary>
	public TooltipPositionType? Type { get; }

	/// <summary>Gets the JavaScript function, if set.</summary>
	public JavascriptFunction? Function { get; }

	/// <summary>Gets the coordinate array position, if set.</summary>
	public NumberOrString[]? Position { get; }

	/// <summary>Implicitly converts a <see cref="TooltipPositionType"/> to a <see cref="TooltipPosition"/>.</summary>
	public static implicit operator TooltipPosition(TooltipPositionType type)
	{
		return new TooltipPosition(type);
	}

	/// <summary>Implicitly converts a <see cref="JavascriptFunction"/> to a <see cref="TooltipPosition"/>.</summary>
	public static implicit operator TooltipPosition(JavascriptFunction function)
	{
		return new TooltipPosition(function);
	}

	/// <summary>Implicitly converts a <see cref="NumberOrString"/> array to a <see cref="TooltipPosition"/>.</summary>
	public static implicit operator TooltipPosition(NumberOrString[] position)
	{
		return new TooltipPosition(position);
	}
}

/// <summary>Named position types for the tooltip.</summary>
public enum TooltipPositionType
{
	/// <summary>The tooltip is shown inside the chart element.</summary>
	Inside,
	/// <summary>The tooltip is shown above the cursor.</summary>
	Top,
	/// <summary>The tooltip is shown to the left of the cursor.</summary>
	Left,
	/// <summary>The tooltip is shown to the right of the cursor.</summary>
	Right,
	/// <summary>The tooltip is shown below the cursor.</summary>
	Bottom
}

/// <summary>JSON converter for <see cref="TooltipPosition"/> that serializes to string, function, or coordinate array.</summary>
public class TooltipPositionConverter : JsonConverter<TooltipPosition>
{
	/// <inheritdoc/>
	public override TooltipPosition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for TooltipPosition.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TooltipPosition value, JsonSerializerOptions options)
	{
		if (value.Type != null)
		{
			writer.WriteStringValue(value.Type!.ToString()!.ToLower());
		}
		else if (value.Position != null)
		{
			writer.WriteStartArray();

			foreach (var val in value.Position)
			{
				NumberOrStringConverter.Instance.Write(writer, val, options);
			}

			writer.WriteEndArray();
		}
		else if (value.Function != null)
		{
			JavascriptFunctionConverter.Instance.Write(writer, value.Function, options);
		}
	}
}
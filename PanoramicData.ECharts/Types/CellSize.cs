using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents the width and height of each cell in a calendar coordinate system.
/// Can be set to a uniform value or separate width and height values.
/// Accepts a number (pixels) or the string "auto" for adaptive sizing.
/// See https://echarts.apache.org/en/option.html#calendar.cellSize
/// </summary>
[JsonConverter(typeof(CellSizeConverter))]
public class CellSize
{
	/// <summary>Creates a <see cref="CellSize"/> with the same value for both width and height.</summary>
	/// <param name="widthAndHeight">The size to use for both width and height (number or "auto").</param>
	public CellSize(NumberOrString widthAndHeight)
	{
		Width = widthAndHeight;
		Height = widthAndHeight;
	}

	/// <summary>Creates a <see cref="CellSize"/> with separate width and height values.</summary>
	/// <param name="width">The cell width (number or "auto").</param>
	/// <param name="height">The cell height (number or "auto").</param>
	public CellSize(NumberOrString width, NumberOrString height)
	{
		Width = width;
		Height = height;
	}

	/// <summary>Gets the cell width.</summary>
	public NumberOrString Width { get; }
	/// <summary>Gets the cell height.</summary>
	public NumberOrString Height { get; }

	/// <summary>Implicitly converts a <see cref="NumberOrString"/> to a <see cref="CellSize"/> with uniform width and height.</summary>
	public static implicit operator CellSize(NumberOrString widthAndHeight)
	{
		return new CellSize(widthAndHeight);
	}
}

/// <summary>JSON converter for <see cref="CellSize"/> that serializes to a two-element array [width, height].</summary>
public class CellSizeConverter : JsonConverter<CellSize>
{
	/// <inheritdoc/>
	public override CellSize Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for CellSize.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, CellSize value, JsonSerializerOptions options)
	{
		writer.WriteStartArray();
		NumberOrStringConverter.Instance.Write(writer, value.Width, options);
		NumberOrStringConverter.Instance.Write(writer, value.Height, options);
		writer.WriteEndArray();
	}
}
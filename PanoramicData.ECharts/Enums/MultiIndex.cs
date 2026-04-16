using System.Text.Json;
using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Represents an axis index value that can be a single index, an array of indices, or a named type (all/none).
/// See https://echarts.apache.org/en/option.html#dataZoom-inside.xAxisIndex
/// </summary>
[JsonConverter(typeof(MultiIndexConverter))]
public class MultiIndex
{
	/// <summary>Creates a <see cref="MultiIndex"/> with a single axis index.</summary>
	/// <param name="index">The axis index.</param>
	public MultiIndex(int index)
	{
		Indices = [index];
	}

	/// <summary>Creates a <see cref="MultiIndex"/> with multiple axis indices.</summary>
	/// <param name="indices">Array of axis indices.</param>
	public MultiIndex(int[] indices)
	{
		Indices = indices;
	}

	/// <summary>Creates a <see cref="MultiIndex"/> from a named type (All or None).</summary>
	/// <param name="type">The named index type.</param>
	public MultiIndex(MultiIndexType type)
	{
		Type = type;
	}

	/// <summary>Gets the array of axis indices, if set.</summary>
	public int[]? Indices { get; }
	/// <summary>Gets the named index type, if set.</summary>
	public MultiIndexType? Type { get; }

	/// <summary>Implicitly converts an integer to a <see cref="MultiIndex"/>.</summary>
	public static implicit operator MultiIndex(int index)
	{
		return new MultiIndex(index);
	}

	/// <summary>Implicitly converts an integer array to a <see cref="MultiIndex"/>.</summary>
	public static implicit operator MultiIndex(int[] indices)
	{
		return new MultiIndex(indices);
	}

	/// <summary>Implicitly converts a <see cref="MultiIndexType"/> to a <see cref="MultiIndex"/>.</summary>
	public static implicit operator MultiIndex(MultiIndexType type)
	{
		return new MultiIndex(type);
	}

	/// <summary>Implicitly converts a string ("none" or "all") to a <see cref="MultiIndex"/>.</summary>
	public static implicit operator MultiIndex(string type)
	{
		return type switch
		{
			"none" => new MultiIndex(MultiIndexType.None),
			"all" => new MultiIndex(MultiIndexType.All),
			_ => throw new NotSupportedException($"'{type}' is not a valid Axis Index")
		};
	}
}

/// <summary>Named index types for <see cref="MultiIndex"/>.</summary>
public enum MultiIndexType
{
	/// <summary>Applies to all axes.</summary>
	All,
	/// <summary>Does not apply to any axis.</summary>
	None
}


/// <summary>JSON converter for <see cref="MultiIndex"/> that serializes to a number, array, or string.</summary>
public class MultiIndexConverter : JsonConverter<MultiIndex>
{
	/// <inheritdoc/>
	public override MultiIndex Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for MultiIndex.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, MultiIndex value, JsonSerializerOptions options)
	{
		if (value.Type != null)
		{
			writer.WriteStringValue(value.Type!.ToString()!.ToLower());
		}
		else if (value.Indices != null)
		{
			if (value.Indices.Length == 1)
			{
				writer.WriteNumberValue(value.Indices[0]);
			}
			else
			{
				writer.WriteStartArray();
				foreach (var val in value.Indices)
				{
					writer.WriteNumberValue(val);
				}

				writer.WriteEndArray();
			}
		}
	}
}
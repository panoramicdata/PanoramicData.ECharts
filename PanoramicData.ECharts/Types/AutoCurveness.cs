using System.Text.Json.Serialization;
using System.Text.Json;

namespace PanoramicData.ECharts;

/// <summary>
/// For the situation where there are multiple links between nodes, the curveness of each link is automatically calculated, not enabled by default.
/// See https://echarts.apache.org/en/option.html#series-graph.autoCurveness
/// </summary>
[JsonConverter(typeof(AutoCurvenessConverter))]
public class AutoCurveness
{
	/// <summary>
	/// When set true to enable automatic curvature calculation, the default edge curvenness array length is 20, if the number of edges between two nodes is more than 20, please use number or Array to set the edge curvenness array.
	/// </summary>
	/// <param name="b"></param>
	public AutoCurveness(bool b)
	{
		Bool = b;
	}

	/// <summary>
	/// When set to number, it indicates the length of the edge curvenness array between two nodes, and the calculation result is given by the internal algorithm.
	/// </summary>
	/// <param name="length"></param>
	public AutoCurveness(double length)
	{
		Length = length;
	}

	/// <summary>
	/// When set to Array, it means that the curveness array is directly specified, and the multilateral curveness is directly selected from the array.
	/// </summary>
	/// <param name="arr"></param>
	public AutoCurveness(double[] arr)
	{
		Array = arr;
	}

	/// <summary>Gets the boolean value, or <c>null</c> if a number or array is set.</summary>
	public bool? Bool { get; }

	/// <summary>Gets the curveness array length, or <c>null</c> if a boolean or array is set.</summary>
	public double? Length { get; }
	/// <summary>Gets the explicit curveness array, or <c>null</c> if a boolean or length is set.</summary>
	public double[]? Array { get; }

	/// <summary>Implicitly converts a bool to an <see cref="AutoCurveness"/>.</summary>
	public static implicit operator AutoCurveness(bool b)
	{
		return new AutoCurveness(b);
	}

	/// <summary>Implicitly converts a double to an <see cref="AutoCurveness"/> with the given array length.</summary>
	public static implicit operator AutoCurveness(double length)
	{
		return new AutoCurveness(length);
	}

	/// <summary>Implicitly converts a double array to an <see cref="AutoCurveness"/> with explicit curveness values.</summary>
	public static implicit operator AutoCurveness(double[] arr)
	{
		return new AutoCurveness(arr);
	}
}

/// <summary>JSON converter for <see cref="AutoCurveness"/> that serializes to a boolean, number, or array.</summary>
public class AutoCurvenessConverter : JsonConverter<AutoCurveness>
{
	/// <inheritdoc/>
	public override AutoCurveness Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException("Deserialization is not implemented for AutoCurveness.");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, AutoCurveness value, JsonSerializerOptions options)
	{
		if (value.Bool != null)
		{
			writer.WriteBooleanValue(value.Bool.Value);
		}
		else if (value.Length != null)
		{
			writer.WriteNumberValue(value.Length.Value);
		}
		else if (value.Array != null)
		{
			writer.WriteStartArray();
			foreach (var val in value.Array)
				writer.WriteNumberValue(val);
			writer.WriteEndArray();
		}
	}
}
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PanoramicData.ECharts;

/// <summary>
/// A <see cref="JsonConverterFactory"/> that creates converters for <see cref="SeriesData{T1,T2}"/> and <see cref="SeriesData{T1,T2,T3}"/> generic types.
/// </summary>
public class SeriesDataConverterFactory : JsonConverterFactory
{
	/// <inheritdoc/>
	public override bool CanConvert(Type typeToConvert)
	{
		if (!typeToConvert.IsGenericType)
			return false;

		var genericDef = typeToConvert.GetGenericTypeDefinition();
		return genericDef == typeof(SeriesData<,>) || genericDef == typeof(SeriesData<,,>);
	}

	/// <inheritdoc/>
	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		Type[] genericArgs = typeToConvert.GetGenericArguments();

		Type converterType = genericArgs.Length switch
		{
			2 => typeof(SeriesDataConverter<,>).MakeGenericType(genericArgs),
			3 => typeof(SeriesDataConverter<,,>).MakeGenericType(genericArgs),
			_ => throw new NotSupportedException($"SeriesDataConverterFactory does not support SeriesData<> with {genericArgs.Length} generic params")
		};

		return (Activator.CreateInstance(converterType) as JsonConverter)!;
	}
}

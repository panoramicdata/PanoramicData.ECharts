using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// Specifies the image format when exporting a chart as an image.
/// See https://echarts.apache.org/en/option.html#toolbox.feature.saveAsImage.type
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<ImageType>))]
public enum ImageType
{
	/// <summary>Export as a PNG image (lossless, supports transparency).</summary>
	Png,
	/// <summary>Export as a JPEG image (lossy compression, smaller file size).</summary>
	Jpg
}

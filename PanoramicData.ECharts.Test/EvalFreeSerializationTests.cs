using AwesomeAssertions;
using Bunit;
using Microsoft.JSInterop;
using System.Text.Json;

namespace PanoramicData.ECharts.Test;

/// <summary>bUnit tests that the options sent to the browser are strict JSON, with functions and data references as markers.</summary>
public class EvalFreeSerializationTests : BunitContext
{
	private (JsonDocument ChartOptions, string? FetchOptions) RenderAndCaptureInit(ChartOptions options, ExternalDataSource[]? dataSources = null)
	{
		JSInterop.Mode = JSRuntimeMode.Loose;
		JSInterop.SetupVoid("panoramicDataECharts.initChart", _ => true).SetVoidResult();

		Render<EChart>(p => p
			.Add(c => c.Options, options)
			.Add(c => c.ExternalDataSources, dataSources));

		var init = JSInterop.Invocations["panoramicDataECharts.initChart"].Should().ContainSingle().Subject;
		return (JsonDocument.Parse((string)init.Arguments[3]!), (string?)init.Arguments[5]);
	}

	/// <summary>Verifies that a registered function is written as a {"$pdFn": name} marker inside valid JSON.</summary>
	[Fact]
	public void RegisteredFunction_IsWrittenAsMarker()
	{
		var options = new ChartOptions
		{
			Tooltip = new() { Formatter = JavascriptFunction.Registered("tooltipFormatter") }
		};

		var (json, _) = RenderAndCaptureInit(options);

		var formatter = json.RootElement.GetProperty("tooltip").GetProperty("formatter");
		formatter.GetProperty("$pdFn").GetString().Should().Be("tooltipFormatter");
		formatter.EnumerateObject().Should().ContainSingle();
	}

	/// <summary>Verifies that a data source reference is a marker, and that a hostile fetch id or path stays a JSON string.</summary>
	[Fact]
	public void ExternalDataSourceRef_IsWrittenAsMarker_EvenWithHostileText()
	{
		const string hostileId = "x');alert(1);//";
		const string hostilePath = "nodes;alert(2)";
		var options = new ChartOptions
		{
			Series = [new GraphSeries { Data = new ExternalDataSourceRef(hostileId, hostilePath) }]
		};

		var (json, _) = RenderAndCaptureInit(options);

		var data = json.RootElement.GetProperty("series")[0].GetProperty("data");
		data.GetProperty("$pdDs").GetString().Should().Be(hostileId);
		data.GetProperty("path").GetString().Should().Be(hostilePath);
	}

	/// <summary>Verifies that an AfterLoad function is sent as its registered name.</summary>
	[Fact]
	public void AfterLoad_IsSentAsRegisteredName()
	{
		var source = new ExternalDataSource("/data/example.json") { AfterLoad = JavascriptFunction.Registered("afterLoadFn") };
		var options = new ChartOptions { Series = [new GraphSeries { Data = new ExternalDataSourceRef(source) }] };

		var (_, fetchOptions) = RenderAndCaptureInit(options, [source]);

		using var fetch = JsonDocument.Parse(fetchOptions!);
		fetch.RootElement[0].GetProperty("afterLoad").GetString().Should().Be("afterLoadFn");
	}

	/// <summary>Verifies that a linear gradient is written as the plain ECharts gradient object.</summary>
	[Fact]
	public void LinearGradient_IsWrittenAsGradientObject()
	{
		var options = new ChartOptions
		{
			Series =
			[
				new BarSeries
				{
					ItemStyle = new()
					{
						Color = new LinearGradient(0, 0, 0, 1, [new GradientColorStop(0, "#83bff6"), new GradientColorStop(1, "#188df0")])
					}
				}
			]
		};

		var (json, _) = RenderAndCaptureInit(options);

		var color = json.RootElement.GetProperty("series")[0].GetProperty("itemStyle").GetProperty("color");
		color.GetProperty("type").GetString().Should().Be("linear");
		color.GetProperty("y2").GetDouble().Should().Be(1);
		color.GetProperty("colorStops")[1].GetProperty("color").GetString().Should().Be("#188df0");
		color.GetProperty("global").GetBoolean().Should().BeFalse();
	}

	/// <summary>Verifies that a gradient without colour stops writes an empty array, which the SVG renderer needs.</summary>
	[Fact]
	public void RadialGradient_WithoutColorStops_WritesEmptyArray()
	{
		var options = new ChartOptions
		{
			Series = [new BarSeries { ItemStyle = new() { Color = new RadialGradient(0.5, 0.5, 0.5) } }]
		};

		var (json, _) = RenderAndCaptureInit(options);

		var color = json.RootElement.GetProperty("series")[0].GetProperty("itemStyle").GetProperty("color");
		color.GetProperty("type").GetString().Should().Be("radial");
		color.GetProperty("colorStops").GetArrayLength().Should().Be(0);
	}

	/// <summary>Verifies that a registered function needs a name.</summary>
	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	public void Registered_RejectsBlankName(string name)
	{
		var act = () => JavascriptFunction.Registered(name);

		act.Should().Throw<ArgumentException>();
	}
}

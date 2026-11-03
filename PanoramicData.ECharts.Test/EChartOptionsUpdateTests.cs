using AwesomeAssertions;
using Bunit;

namespace PanoramicData.ECharts.Test;

/// <summary>bUnit tests for how <see cref="EChart"/> applies <see cref="EChartBase.Options"/> changes.</summary>
public class EChartOptionsUpdateTests : BunitContext
{
	/// <summary>Verifies that Options set while the first initChart call is still pending are drawn once it completes.</summary>
	[Fact]
	public async Task OptionsChangedDuringInit_AreDrawnAfterInitCompletes()
	{
		JSInterop.Mode = JSRuntimeMode.Loose;
		var init = JSInterop.SetupVoid("panoramicDataECharts.initChart", _ => true);

		var initialOptions = new ChartOptions { Title = new() { Text = "initial-options" } };
		var updatedOptions = new ChartOptions { Title = new() { Text = "updated-options" } };

		var cut = Render<EChart>(p => p.Add(c => c.Options, initialOptions));

		// initChart is still pending when the parent supplies new options
		cut.Render(p => p.Add(c => c.Options, updatedOptions));
		await cut.InvokeAsync(init.SetVoidResult);

		cut.WaitForAssertion(() =>
		{
			var updates = JSInterop.Invocations["panoramicDataECharts.updateChart"];
			updates.Should().ContainSingle();
			((string)updates[0].Arguments[1]!).Should().Contain("updated-options");
		});
		JSInterop.Invocations["panoramicDataECharts.initChart"].Should().ContainSingle();
	}
}

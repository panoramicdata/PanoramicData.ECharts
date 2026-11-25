using AwesomeAssertions;
using Bunit;
using Microsoft.JSInterop;

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

	/// <summary>Verifies that changes made in place to the same Options instance while initChart is pending are drawn once it completes.</summary>
	[Fact]
	public async Task OptionsMutatedInPlaceDuringInit_AreDrawnAfterInitCompletes()
	{
		JSInterop.Mode = JSRuntimeMode.Loose;
		var init = JSInterop.SetupVoid("panoramicDataECharts.initChart", _ => true);

		var options = new ChartOptions { Title = new() { Text = "initial-title" } };

		var cut = Render<EChart>(p => p.Add(c => c.Options, options));

		// the parent changes the instance it already passed, then re-renders
		options.Title.Text = "mutated-title";
		cut.Render(p => p.Add(c => c.Options, options));
		await cut.InvokeAsync(init.SetVoidResult);

		cut.WaitForAssertion(() =>
		{
			var updates = JSInterop.Invocations["panoramicDataECharts.updateChart"];
			updates.Should().ContainSingle();
			((string)updates[0].Arguments[1]!).Should().Contain("mutated-title");
		});
		JSInterop.Invocations["panoramicDataECharts.initChart"].Should().ContainSingle();
	}

	/// <summary>Verifies that with a DataLoader, changes made in place while the initial updateChart is pending are drawn once it completes.</summary>
	[Fact]
	public async Task OptionsMutatedInPlaceDuringDataLoaderInit_AreDrawnAfterInitCompletes()
	{
		JSInterop.Mode = JSRuntimeMode.Loose;
		var update = JSInterop.SetupVoid("panoramicDataECharts.updateChart", _ => true);

		var options = new ChartOptions { Title = new() { Text = "initial-title" } };

		var cut = Render<EChart>(p => p
			.Add(c => c.Options, options)
			.Add(c => c.DataLoader, () => { }));

		// the data has been loaded and drawn, but that updateChart is still pending
		JSInterop.Invocations["panoramicDataECharts.updateChart"].Should().ContainSingle();
		options.Title.Text = "mutated-title";
		cut.Render(p => p.Add(c => c.Options, options));
		await cut.InvokeAsync(update.SetVoidResult);

		cut.WaitForAssertion(() =>
		{
			var updates = JSInterop.Invocations["panoramicDataECharts.updateChart"];
			updates.Should().HaveCount(2);
			((string)updates[1].Arguments[1]!).Should().Contain("mutated-title");
		});
		JSInterop.Invocations["panoramicDataECharts.initChart"].Should().ContainSingle();
	}

	/// <summary>Verifies that when Options did not change during init, no extra updateChart call is made.</summary>
	[Fact]
	public async Task OptionsUnchangedDuringInit_AreNotRedrawn()
	{
		JSInterop.Mode = JSRuntimeMode.Loose;
		var init = JSInterop.SetupVoid("panoramicDataECharts.initChart", _ => true);

		var options = new ChartOptions { Title = new() { Text = "initial-title" } };

		var cut = Render<EChart>(p => p.Add(c => c.Options, options));

		cut.Render(p => p.Add(c => c.Options, options));
		await cut.InvokeAsync(init.SetVoidResult);

		JSInterop.Invocations["panoramicDataECharts.initChart"].Should().ContainSingle();
		JSInterop.Invocations["panoramicDataECharts.updateChart"].Should().BeEmpty();
	}

	/// <summary>Verifies that UpdateAsync swallows a JS disconnect, since there is no browser chart left to update.</summary>
	[Fact]
	public async Task UpdateAsync_WhenCircuitDisconnected_DoesNotThrow()
	{
		await AssertUpdateAsyncSwallows(new JSDisconnectedException("disconnected"));
	}

	/// <summary>Verifies that UpdateAsync swallows an operation cancelled while the update was in flight.</summary>
	[Fact]
	public async Task UpdateAsync_WhenCancelled_DoesNotThrow()
	{
		await AssertUpdateAsyncSwallows(new TaskCanceledException());
	}

	/// <summary>Verifies that UpdateAsync swallows a disposed runtime.</summary>
	[Fact]
	public async Task UpdateAsync_WhenDisposed_DoesNotThrow()
	{
		await AssertUpdateAsyncSwallows(new ObjectDisposedException("runtime"));
	}

	/// <summary>Verifies that UpdateAsync still surfaces a genuine JavaScript error.</summary>
	[Fact]
	public async Task UpdateAsync_WhenJavaScriptFails_Throws()
	{
		var (cut, update) = await RenderInitialisedAsync();
		update.SetException(new JSException("boom"));

		var act = () => cut.InvokeAsync(() => cut.Instance.UpdateAsync(false));

		await act.Should().ThrowAsync<JSException>();
	}

	private async Task AssertUpdateAsyncSwallows(Exception exception)
	{
		var (cut, update) = await RenderInitialisedAsync();
		update.SetException(exception);

		var act = () => cut.InvokeAsync(() => cut.Instance.UpdateAsync(false));

		await act.Should().NotThrowAsync();
	}

	private async Task<(IRenderedComponent<EChart> Cut, JSRuntimeInvocationHandler Update)> RenderInitialisedAsync()
	{
		JSInterop.Mode = JSRuntimeMode.Loose;
		var update = JSInterop.SetupVoid("panoramicDataECharts.updateChart", _ => true);
		var cut = Render<EChart>(p => p.Add(c => c.Options, new ChartOptions { Title = new() { Text = "t" } }));
		await cut.WaitForStateAsync(() => JSInterop.Invocations["panoramicDataECharts.initChart"].Count > 0);
		return (cut, update);
	}
}

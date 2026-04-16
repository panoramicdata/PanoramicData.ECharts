using AwesomeAssertions;
using Microsoft.Playwright;

namespace PanoramicData.ECharts.Test;

/// <summary>Base class providing shared Playwright browser setup for ECharts rendering tests.</summary>
public class TestBase : IAsyncLifetime
{
	/// <summary>Gets the base URL of the demo application under test.</summary>
	protected const string BaseUrl = "http://localhost:5185/example"; // Fixed: added /example prefix
	/// <summary>Gets the default timeout in milliseconds for Playwright operations.</summary>
	protected const int DefaultTimeout = 20000; // 20 seconds - increased for chart initialization

	/// <summary>Gets the Playwright instance used for browser automation.</summary>
	protected IPlaywright? Playwright { get; private set; }
	/// <summary>Gets the browser instance used for tests.</summary>
	protected IBrowser? Browser { get; private set; }
	/// <summary>Gets the browser context for isolating test state.</summary>
	protected IBrowserContext? Context { get; private set; }
	/// <summary>Gets the page used to navigate and interact with the demo application.</summary>
	protected IPage Page { get; private set; } = null!;

	/// <summary>Initializes Playwright, launches a browser, and creates a page before each test.</summary>
	public async ValueTask InitializeAsync()
	{
		// Install playwright
		var exitCode = Microsoft.Playwright.Program.Main(["install"]);
		if (exitCode != 0)
		{
			throw new Exception($"Playwright install failed with exit code {exitCode}");
		}

		// Create playwright instance
		Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

		// Launch browser
		Browser = await Playwright.Chromium.LaunchAsync(new()
		{
			Headless = true
		});

		// Create context
		Context = await Browser.NewContextAsync(new()
		{
			ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
			IgnoreHTTPSErrors = true,
		});

		// Create page
		Page = await Context.NewPageAsync();
		Page.SetDefaultTimeout(DefaultTimeout);
	}

	/// <summary>Closes the page, browser context, browser, and Playwright instance after each test.</summary>
	public async ValueTask DisposeAsync()
	{
		if (Page != null)
		{
			await Page.CloseAsync();
		}

		if (Context != null)
		{
			await Context.CloseAsync();
		}

		if (Browser != null)
		{
			await Browser.CloseAsync();
		}

		Playwright?.Dispose();
	}

	/// <summary>
	/// Wait for chart to be initialized and rendered
	/// </summary>
	protected async Task WaitForChartAsync()
	{
		// Wait for Blazor to finish loading
		await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

		// Wait for chart container DIV to appear
		await Page.WaitForSelectorAsync("[id^='chart']", new()
		{
			State = WaitForSelectorState.Attached
		});

		// CRITICAL: Wait for ECharts to create the canvas or SVG inside the div
		// ECharts can use either canvas or SVG renderer
		await Page.WaitForSelectorAsync("[id^='chart'] canvas, [id^='chart'] svg", new()
		{
			State = WaitForSelectorState.Visible,
			Timeout = 15000  // Give ECharts time to initialize
		});

		// Give ECharts animations time to complete
		await Page.WaitForTimeoutAsync(500);
	}

	/// <summary>
	/// Verify no JavaScript console errors
	/// </summary>
	protected async Task<List<string>> GetConsoleErrors()
	{
		var errors = new List<string>();

		Page.Console += (_, msg) =>
		{
			if (msg.Type == "error")
			{
				errors.Add(msg.Text);
			}
		};

		await Page.WaitForTimeoutAsync(500);
		return errors;
	}

	/// <summary>
	/// Verify ECharts global object exists with correct name
	/// </summary>
	protected async Task VerifyEChartsGlobalAsync()
	{
		var hasGlobal = await Page.EvaluateAsync<bool>(
			"() => typeof window.panoramicDataECharts !== 'undefined'"
		);
		hasGlobal.Should().BeTrue("window.panoramicDataECharts not found");

		var oldGlobal = await Page.EvaluateAsync<bool>(
			"() => typeof window.vizorECharts !== 'undefined'"
		);
		oldGlobal.Should().BeFalse("Old window.vizorECharts still exists");

		var version = await Page.EvaluateAsync<string>("() => echarts.version");
		version.Should().Be("6.0.0");
	}

	/// <summary>
	/// Verify chart canvas or SVG is visible
	/// </summary>
	protected async Task<bool> IsChartVisibleAsync() => await Page.Locator("[id^='chart'] canvas, [id^='chart'] svg").IsVisibleAsync();

	/// <summary>
	/// Take a screenshot for the current test
	/// </summary>
	protected async Task TakeScreenshotAsync(string filename)
	{
		var screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "screenshots");
		Directory.CreateDirectory(screenshotsDir);

		var path = Path.Combine(screenshotsDir, filename);
		await Page.ScreenshotAsync(new() { Path = path, FullPage = false });
	}
}

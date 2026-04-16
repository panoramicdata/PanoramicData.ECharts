using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PanoramicData.ECharts.Demo.Pages
{
	/// <summary>Page model for the error page, displaying request ID information.</summary>
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	[IgnoreAntiforgeryToken]
	public class ErrorModel : PageModel
	{
		/// <summary>Gets or sets the current request ID for diagnostic purposes.</summary>
		public string? RequestId { get; set; }

		/// <summary>Gets a value indicating whether a request ID is available to display.</summary>
		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

		private readonly ILogger<ErrorModel> _logger;

		/// <summary>Initializes a new instance of <see cref="ErrorModel"/> with the specified logger.</summary>
		public ErrorModel(ILogger<ErrorModel> logger)
		{
			_logger = logger;
		}

		/// <summary>Handles the GET request, capturing the current activity or trace identifier.</summary>
		public void OnGet() => RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
	}
}
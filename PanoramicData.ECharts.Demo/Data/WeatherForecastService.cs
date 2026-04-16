namespace PanoramicData.ECharts.Demo.Data
{
	/// <summary>Provides simulated weather forecast data for the demo application.</summary>
	public class WeatherForecastService
	{
		private static readonly string[] Summaries =
		[
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	];

		/// <summary>Returns an array of randomly generated weather forecasts starting from the specified date.</summary>
		public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate) => Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = startDate.AddDays(index),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		}).ToArray());
	}
}
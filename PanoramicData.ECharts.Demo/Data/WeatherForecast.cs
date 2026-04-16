namespace PanoramicData.ECharts.Demo.Data
{
	/// <summary>Represents a weather forecast for a specific date.</summary>
	public class WeatherForecast
	{
		/// <summary>Gets or sets the forecast date.</summary>
		public DateOnly Date { get; set; }

		/// <summary>Gets or sets the temperature in degrees Celsius.</summary>
		public int TemperatureC { get; set; }

		/// <summary>Gets the temperature in degrees Fahrenheit, computed from <see cref="TemperatureC"/>.</summary>
		public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

		/// <summary>Gets or sets a short summary of the forecast conditions.</summary>
		public string? Summary { get; set; }
	}
}
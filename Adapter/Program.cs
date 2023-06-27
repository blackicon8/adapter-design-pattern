using Adapter.Adapter;
using Adapter.Common.Models;
using Adapter.Common.Services;

var legacyWeatherApp = new LegacyWeatherApp();
var newWeatherApi = new ThirdPartyWeatherApi();

var dateRange = new DateRange("2023.01.01", "2023.01.10");

var weatherStats = new WeatherApiAdapter(newWeatherApi)
                    .GetWeatherStats("Budapest", dateRange);

legacyWeatherApp.WriteDailyWeather(weatherStats);


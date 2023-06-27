using Adapter.Common.Interfaces;
using Adapter.Common.Models;
using Adapter.Common.Services;
using System.Text.Json;

namespace Adapter.Adapter
{
    public class WeatherApiAdapter : IWeatherDataProvider
    {
        private readonly ThirdPartyWeatherApi _weatherApi;

        public WeatherApiAdapter(ThirdPartyWeatherApi weatherApi)
        {
            _weatherApi = weatherApi;
        }

        public WeatherStats GetWeatherStats(string city, DateRange dateRange)
        {
            var json = _weatherApi.GetWeatherStatistics(city, dateRange);
            var facts = JsonSerializer.Deserialize<List<WeatherFact>>(json);

            return 
                new WeatherStats()
                {
                    City = city,
                    Facts = facts
                };
        }
    }
}

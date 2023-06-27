using Adapter.Common.Models;

namespace Adapter.Common.Services
{
    public class LegacyWeatherApp
    {
        public void WriteDailyWeather(WeatherStats weatherStats)
        {
            ContextConsole.WriteDailyWeather(weatherStats);
        }
    }
}

using Adapter.Common.Models;

namespace Adapter.Common.Interfaces
{
    public interface IWeatherDataProvider
    {
        public WeatherStats GetWeatherStats(string city, DateRange dateRange);
    }
}

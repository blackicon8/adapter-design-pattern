using Adapter.Common.Models;
using System.Text.Json;

namespace Adapter.Common.Services
{
    public class ThirdPartyWeatherApi
    {
        private static readonly Random random = new Random();

        public string GetWeatherStatistics(string city, DateRange dateRange)
        {
            var weatherFacts = new List<WeatherFact>();

            foreach (var date in dateRange)
            {
                var weatherFact = new WeatherFact
                {
                    Date = date,
                    MaxTemperature = GetRandomDouble(25, 40),
                    Humidity = GetRandomDouble(0, 50),
                    Description = GetRandomDescription()
                };

                var max = weatherFact.MaxTemperature;
                weatherFact.MinTemperature = 
                    GetRandomDouble(max - 15, max - 5);

                weatherFacts.Add(weatherFact);
            }

            return JsonSerializer.Serialize(weatherFacts);
        }

        private static double GetRandomDouble(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }

        private static string GetRandomDescription()
        {
            string[] descriptions = { "Sunny", "Cloudy", "Partly Cloudy", "Rainy", "Windy" };
            return descriptions[random.Next(descriptions.Length)];
        }
    }
}

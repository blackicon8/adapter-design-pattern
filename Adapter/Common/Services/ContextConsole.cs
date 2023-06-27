using Adapter.Common.Models;
using System.Globalization;

namespace Adapter.Common.Services
{
    public class ContextConsole
    {
        private static readonly CultureInfo _culture = CultureInfo.InvariantCulture;
        private static readonly string _format = "0.0";

        public static void WriteDailyWeather(WeatherStats weatherStats)
        {
            Header(weatherStats.City);

            foreach (var fact in weatherStats.Facts)
            {
                DailyWeather(fact);
            }
        }

        private static void Header(string city)
        {
            SeparatorLine();
            Console.WriteLine($"{city} weather");
            SeparatorLine();
        }

        private static void DailyWeather(WeatherFact fact)
        {
            Console.WriteLine($"{fact.Date.ToString("yyyy/MM/dd")} ({fact.Date.ToString("ddd", _culture)})");
            EmptyLine();
            Console.WriteLine($"{fact.Description}");
            EmptyLine();
            Console.WriteLine($"Minimum:   {fact.MinTemperature.ToString(_format)} °C");
            Console.WriteLine($"Maximum:   {fact.MaxTemperature.ToString(_format)} °C");
            Console.WriteLine($"Humidity:  {fact.Humidity.ToString(_format)} %");
            SeparatorLine();
        }

        private static void SeparatorLine()
        {
            Console.WriteLine("=========================================");
        }

        private static void EmptyLine()
        {
            Console.WriteLine();
        }
    }
}

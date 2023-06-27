namespace Adapter.Common.Models
{
    public class WeatherFact
    {
        public DateTime Date { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double Humidity { get; set; }
        public string Description { get; set; }
    }
}

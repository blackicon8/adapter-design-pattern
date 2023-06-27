namespace Adapter.Common.Models
{
    public class WeatherStats
    {
        public string City{ get; set; }
        public IList<WeatherFact> Facts { get; set; }
    }
}

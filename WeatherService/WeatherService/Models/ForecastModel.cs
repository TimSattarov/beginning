using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherService.Models
{
    public class ForecastModel
    {
        public string Date { get; set; }
        public string City { get; set; }
        public string Temperature { get; set; }
        public string TemperatureMetric { get; set; }
    }
}

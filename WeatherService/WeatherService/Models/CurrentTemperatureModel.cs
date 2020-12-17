using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherService.Models
{
    public class CurrentTemperatureModel
    {
        public string City { get; set; }

        public double Temperature { get; set; }

        public string Metric { get; set; }
    }
}

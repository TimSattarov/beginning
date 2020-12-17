using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherService.Models
{
    public class CurrentWindModel
    {
        public string City { get; set; }

        public double Speed { get; set; }

        public string Direction { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeatherService.Models
{
    [Serializable, XmlRoot("current")]
    public class OpenWeatherCurrentResponse
    {
        [XmlElement("temperature")]
        public CurrentTemperature Temperature { get; set; }

        [XmlElement("wind")]
        public CurrentWind Wind { get; set; }

        [XmlElement("city")]
        public CurrentCity City { get; set; }
    }



    public class CurrentTemperature
    {
        [XmlAttribute("value")]
        public double Value { get; set; }
        [XmlAttribute("unit")]
        public string Unit { get; set; }
    }



    public class CurrentWind
    {
        [XmlElement("speed")]
        public Speed Speed { get; set; }
        [XmlElement("direction")]
        public Direction Direction { get; set; }
    }


    public class Speed
    {
        [XmlAttribute("value")]
        public double Value { get; set; }
    }

    public class Direction
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }



    public class CurrentCity
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}

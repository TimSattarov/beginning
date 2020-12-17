using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeatherService.Models
{
    [Serializable, XmlRoot("weatherdata")]
    public class OpenWeatherForecastResponse
    {
        [XmlElement("location")]
        public ForecastCity City { get; set; }

        [XmlArray("forecast")]
        [XmlArrayItem(typeof(ForecastDay), ElementName = "time")]
        public List<ForecastDay> Forecast { get; set; }
    }



    public class ForecastCity
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }

    public class ForecastDay
    {
        [XmlAttribute("from")]
        public string from { get; set; }
        [XmlAttribute("to")]
        public string to { get; set; }
        [XmlElement("temperature")]
        public ForecastTemperature Temperature { get; set; }
    }

    public class ForecastTemperature
    {
        [XmlAttribute("value")] 
        public double value { get; set; }

        [XmlAttribute("unit")]
        public string unit { get; set; }


    }
}

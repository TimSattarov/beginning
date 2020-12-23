using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using RestSharp;
using WeatherService.Models;

namespace WeatherService.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly string _apiKey;

        public WeatherService(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("OpenWeatherApiKey");
        }


        public async Task<CurrentTemperatureModel> GetTemperature(string cityName, string metric)
        {
            XDocument xDoc = GetXml(cityName, metric, "weather");

            var temperature = new CurrentTemperatureModel
            {
                City = xDoc.Element("current").Element("city").Attribute("name").Value,
                Temperature = double.Parse(xDoc.Element("current").Element("temperature").Attribute("value").Value),
                Metric = xDoc.Element("current").Element("temperature").Attribute("unit").Value,
            };

            return temperature;
        }



        public async Task<CurrentWindModel> GetWind(string cityName)
        {
            XDocument xDoc = GetXml(cityName, "metric", "weather");

            var wind = new CurrentWindModel
            {
                City = xDoc.Element("current").Element("city").Attribute("name").Value,
                Speed = double.Parse(xDoc.Element("current").Element("wind").Element("speed").Attribute("value").Value),
                Direction = xDoc.Element("current").Element("wind").Element("direction").Attribute("name").Value
            };

            return wind;
        }


        public async Task<IEnumerable<ForecastModel>> GetForecast(string cityName, string metric)
        {
            XDocument xdoc = GetXml(cityName, metric, "forecast");
            var result = new List<ForecastModel>();
            var city = xdoc.Element("weatherdata").Element("location").Element("name").Value;

            foreach (XElement item in xdoc.Element("weatherdata").Element("forecast").Elements("time"))
            {
                result.Add(new ForecastModel()
                {
                    Date = $"from {item.Attribute("from").Value} to {item.Attribute("to").Value}",
                    City = city,
                    Temperature = item.Element("temperature").Attribute("value").Value,
                    TemperatureMetric = item.Element("temperature").Attribute("unit").Value
                });
            }

            return result;
        }



        private XDocument GetXml(string cityName, string metric, string Uri)
        {
            if (metric == "celsius") metric = "metric";
            if (metric == "fahrenheit") metric = "imperial";

            string baseAdress = $"http://api.openweathermap.org/data/2.5/{Uri}?q={cityName}&appid={_apiKey}&lang=ru&units={metric}&mode=xml";

            var client = new RestClient(baseAdress);
            var request = new RestRequest(Method.GET)
            {
                RequestFormat = DataFormat.Xml,
            };
            var response = client.Execute(request);

            XDocument xDoc = XDocument.Parse(response.Content);
            return xDoc;
        }
    }
}

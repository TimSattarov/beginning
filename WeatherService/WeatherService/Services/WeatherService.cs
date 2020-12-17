using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WeatherService.Clients;
using WeatherService.Models;

namespace WeatherService.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherClient _openWeather;
        private readonly string _apiKey;

        public WeatherService(IOpenWeatherClient openWeather, IConfiguration configuration)
        {
            _openWeather = openWeather;
            _apiKey = configuration.GetValue<string>("OpenWeatherApiKey");
        }



        public async Task<CurrentTemperatureModel> GetTemperature(string cityName, string metric)
        {
            if (metric == "celsius") metric = "metric";
            if (metric == "fahrenheit") metric = "imperial";
            var response = await _openWeather.GetWeather(cityName, metric, _apiKey);

            var temperature = new CurrentTemperatureModel
            {
                City = response.City.Name,
                Temperature = response.Temperature.Value,
                Metric = response.Temperature.Unit
            };

            return temperature;
        }



        public async Task<CurrentWindModel> GetWind(string cityName)
        {
            var response = await _openWeather.GetWeather(cityName, "metric", _apiKey);

            var wind = new CurrentWindModel
            {
                City = response.City.Name,
                Speed = response.Wind.Speed.Value,
                Direction = response.Wind.Direction.Name
            };

            return wind;
        }


        public async Task<IEnumerable<ForecastModel>> GetForecast(string cityName, string metric)
        {
            if (metric == "celsius") metric = "metric";
            if (metric == "fahrenheit") metric = "imperial";

            var response = await _openWeather.GetForecast(cityName, metric, _apiKey);

            var result = new List<ForecastModel>();

            for (int i = 0; i < response.Forecast.Count; i++)
            {
                result.Add(new ForecastModel()
                {
                    Date = $"from {response.Forecast[i].from} to {response.Forecast[i].to}",
                    City = response.City.Name,
                    Temperature = response.Forecast[i].Temperature.value,
                    TemperatureMetric = response.Forecast[i].Temperature.unit
                });
            }

            return result;
        }
    }
}

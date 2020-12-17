using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherService.Models;

namespace WeatherService.Services
{
    public interface IWeatherService
    {
        Task<CurrentTemperatureModel> GetTemperature(string cityName, string metric);
        Task<CurrentWindModel> GetWind(string cityName);
        Task<IEnumerable<ForecastModel>> GetForecast(string cityName, string metric);
    }
}

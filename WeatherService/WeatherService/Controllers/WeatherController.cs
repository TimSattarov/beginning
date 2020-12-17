using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WeatherService.Models;
using WeatherService.Services;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }



        [HttpGet("temperature/{cityName}/{metric}")]
        public async Task<CurrentTemperatureModel> GetTemperature(string cityName, string metric)
        {
            return await _weatherService.GetTemperature(cityName, metric);
        }



        [HttpGet("wind/{cityName}")]
        public async Task<CurrentWindModel> GetWind(string cityName)
        {
            return await _weatherService.GetWind(cityName);
        }



        [HttpGet("{cityName}/future/{metric}")]
        public async Task<IEnumerable<ForecastModel>> GetForecast(string cityName, string metric)
        {
            return await _weatherService.GetForecast(cityName, metric);
        }
    }
}

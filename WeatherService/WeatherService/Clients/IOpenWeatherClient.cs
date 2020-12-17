using Refit;
using System.Threading.Tasks;
using WeatherService.Models;

namespace WeatherService.Clients
{
    public interface IOpenWeatherClient
    {
        [Get("/weather?q={cityName}&units={metric}&mode=xml&lang=ru&appid={ApiKey}")]
        Task<OpenWeatherCurrentResponse> GetWeather(string cityName, string metric, string ApiKey);


        [Get("/forecast?q={cityName}&units={metric}&mode=xml&lang=ru&appid={ApiKey}")]
        Task<OpenWeatherForecastResponse> GetForecast(string cityName, string metric, string ApiKey);
    }
}

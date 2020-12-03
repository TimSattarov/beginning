using Microsoft.Extensions.Configuration;
using ImageService.Clients;
using ImageService.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageService.Interfaces;

namespace ImageService.Services
{
    public class YaDiskService : IYaDiskService
    {
        private readonly IPoligonClient _poligonClient;
        private readonly string _token;



        public YaDiskService(IPoligonClient poligonClient, IConfiguration configuration)
        {
            _token = configuration.GetValue<string>("YaDiskToken");
            _poligonClient = poligonClient;
        }




        public async Task<string> GetUrl(string pathOnYaDisk)
        {
            //Получение метаинформации о файле (public_url)
            var response = await _poligonClient.GetFileInfo(pathOnYaDisk, _token);
            return response.Public_url;
        }

        public async Task<string> PostImage(string pathOnSystem)
        {
            //Получили имя файла, расположенного на локальном диске и присвоили имя новому файлу на ЯндексДиске
            var fileName = Path.GetFileName(pathOnSystem);
            string pathOnYaDisk = $"disk:/test/{fileName}";

            //Получили Url для загрузки файла на ЯндексДиск
            var upload = await _poligonClient.GetHref(pathOnYaDisk, _token);

            //Кодируем файл в массив байтов и загружаем на ЯндексДИск
            byte[] bData = File.ReadAllBytes(pathOnSystem);
            var client = new RestClient(upload.Href);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", _token);
            request.AddFile("file", bData, "");
            client.Execute(request);

            //Делаем файл на ЯндексДиске публичным
            await _poligonClient.PublishFile(pathOnYaDisk, _token);

            return pathOnYaDisk;
        }
    }
}

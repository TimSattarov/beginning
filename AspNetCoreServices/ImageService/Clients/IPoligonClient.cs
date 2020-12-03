using System.Collections.Generic;
using System.Threading.Tasks;
using ImageService.Models;
using Refit;

namespace ImageService.Clients
{
    public interface IPoligonClient
    {
        [Get("/resources/?path={path}")]
        Task<PoligonModel> GetFileInfo(string path, [Header("Authorization")] string authorization);


        [Get("/resources/upload?path={path}")]
        Task<PoligonModel> GetHref(string path, [Header("Authorization")] string authorization);


        [Put("/resources/publish")]
        Task PublishFile(string path, [Header("Authorization")] string authorization);
    }
}

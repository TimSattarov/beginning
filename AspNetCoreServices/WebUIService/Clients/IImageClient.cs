using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebUIService.Models;
using Refit;

namespace WebUIService.Clients
{
    public interface IImageClient
    {
        [Get("/api/image")]
        Task<IEnumerable<ImageModel>> GetAll();

        [Post("/api/image")]
        Task CreateMany(IEnumerable<ImageModel> images);
    }
}
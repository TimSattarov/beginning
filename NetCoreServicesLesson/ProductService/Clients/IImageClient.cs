using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Models;
using Refit;

namespace ProductService.Clients
{
    public interface IImageClient
    {
        [Get("/api/image")]
        Task<IEnumerable<ImageModel>> GetAll();
    }
}
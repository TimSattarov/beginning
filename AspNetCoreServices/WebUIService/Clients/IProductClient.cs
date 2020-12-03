using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebUIService.Models;
using Refit;

namespace WebUIService.Clients
{
    public interface IProductClient
    {
        [Get("/api/product")]
        Task<IEnumerable<ProductModel>> GetAll();

        [Post("/api/product")]
        Task CreateMany(IEnumerable<ProductModel> products);
    }
}
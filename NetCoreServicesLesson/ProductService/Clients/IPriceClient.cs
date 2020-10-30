using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Models;
using Refit;

namespace ProductService.Clients
{
    public interface IPriceClient
    {
        [Get("/api/price")]
        Task<IEnumerable<PriceModel>> GetAll();
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Repository;

namespace PriceService.Repositories
{
    public interface IPriceRepository : IBaseRepository<Price>
    {
        Task<IEnumerable<Price>> GetAll();
    }
}
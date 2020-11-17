using Microsoft.Extensions.Options;
using Repository;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PriceService.Repositories
{
    public class PriceRepository : BaseRepository<Price>, IPriceRepository
    {
        public PriceRepository(IOptions<PriceDbOptions> dbOptions) 
            : base (dbOptions)
        {
        }
    }
}
using Microsoft.Extensions.Options;
using repository;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace PriceService.Repositories
{
    public class PriceRepository : BaseRepository<PriceDbModel>, IPriceRepository
    {
        public PriceRepository(IOptions<PriceDbOptions> dbOptions) : base (dbOptions)
        {
        }
        
        public override async Task<IEnumerable<PriceDbModel>> GetAll()
        {
            await using var db = await GetSqlConnection();
            return await db.QueryAsync<PriceDbModel>($"SELECT * FROM [Price] WHERE [IsDeleted] = 0");
        }
    }
}
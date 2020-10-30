using PriceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceService.Services
{
    public class PriceService : IPriceService
    {
        public IEnumerable<PriceModel> _prices = new List<PriceModel>
        {
            new PriceModel() {Id = 0, CurrentPrice = 990.0f, SalePrice = 985.0f, RRPrice = 987.0f, ProductId = 0},
            new PriceModel() {Id = 1, CurrentPrice = 300.0f, SalePrice = 285.0f, RRPrice = 310.0f, ProductId = 2},
            new PriceModel() {Id = 2, CurrentPrice = 1200.0f, SalePrice = 1200.0f, RRPrice = 1150.0f, ProductId = 3},
            new PriceModel() {Id = 3, CurrentPrice = 1500.0f, SalePrice = 1499.0f, RRPrice = 1450.0f, ProductId = 4}
        };

        public IEnumerable<PriceModel> GetAll() => _prices;

        public PriceModel Get(int id) => _prices.FirstOrDefault(p => p.Id == id);
    }
}

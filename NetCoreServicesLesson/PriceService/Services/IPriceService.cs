using PriceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceService.Services
{
    public interface IPriceService
    {
        IEnumerable<PriceModel> GetAll();
        PriceModel Get(int id);
    }
}

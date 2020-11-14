using ImageService.Clients;
using ImageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService.Services
{
    public class PoligonService
    {
        private readonly IPoligonClient _poligonClient;
        public PoligonService(IPoligonClient poligonClient)
        {
            _poligonClient = poligonClient;
        }


    }
}

using ImageService.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService.Clients
{
    public interface IPoligonClient
    {
        [Get("/resources/files")]
        [Headers("Authorization: OAuth")]
        Task<PoligonModel> GetAll();
    }
}

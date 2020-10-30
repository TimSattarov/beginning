using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceService.Models;
using PriceService.Services;

namespace PriceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }


        [HttpGet("{id}")]
        public ActionResult<PriceModel> Get(int id)
        {
            var model = _priceService.Get(id);

            if (model == null)
            {
                return BadRequest("Product not found");
            }

            return Ok(model);
        }


        [HttpGet]
        public ActionResult<IEnumerable<PriceModel>> GetAll()
        {
            var collection = _priceService.GetAll();

            if (collection == null)
            {
                return BadRequest("Products not found");
            }

            return Ok(collection);
        }
    }
}

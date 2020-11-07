using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PriceService.Models;
using PriceService.Repositories;


namespace PriceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceRepository _priceRepository;
        private readonly ILogger<PriceController> _logger;
        private readonly IMapper _mapper;

        public PriceController(IPriceRepository priceRepository, ILogger<PriceController> logger, IMapper mapper)
        {
            _priceRepository = priceRepository;
            _logger = logger;
            _mapper = mapper;
        }


        // [HttpGet("{id}")]
        // public ActionResult<PriceModel> Get(int id)
        // {
        //     var model = _priceRepository.Get(id);

        //     if (model == null)
        //     {
        //         return BadRequest("Prices not found");
        //     }

        //     return Ok(model);
        // }


        [HttpGet]
        public async Task<IEnumerable<PriceModel>> GetAll()
        {
            var priceDbModels = await _priceRepository.GetAll();
            var prices = _mapper.Map<IEnumerable<PriceModel>>(priceDbModels);

            return prices;
        }
    }
}

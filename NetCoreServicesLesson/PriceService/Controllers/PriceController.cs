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

        [HttpGet]
        public async Task<IEnumerable<PriceModel>> GetAll()
        {
            var priceDbModels = await _priceRepository.GetAll();
            var prices = _mapper.Map<IEnumerable<PriceModel>>(priceDbModels);
            return prices;
        }

        [HttpGet("{id}")]
        public async Task<PriceModel> GetById(Guid id)
        {
            var priceDbModel = await _priceRepository.GetById(id);
            var price = _mapper.Map<PriceModel>(priceDbModel);
            return price;
        }
        

        [HttpPost]
        public async Task CreateMany(IEnumerable<PriceModel> prices)
        {
            var priceDbModels = _mapper.Map<IEnumerable<Price>>(prices);
            await _priceRepository.CreateMany(priceDbModels);
        }

        [HttpPost("{id}")]
        public async Task Create(PriceModel price)
        {
            var priceDbModel = _mapper.Map<Price>(price);
            await _priceRepository.Create(priceDbModel);
        }


        [HttpPut]
        public async Task UpdateMany(IEnumerable<PriceModel> prices)
        {
            var priceDbModels = _mapper.Map<IEnumerable<Price>>(prices);
            await _priceRepository.UpdateMany(priceDbModels);
        }

        [HttpPut("{id}")]
        public async Task Update(PriceModel price)
        {
            var priceDbModel = _mapper.Map<Price>(price);
            await _priceRepository.Update(priceDbModel);
        }


        [HttpDelete]
        public async Task DeleteMany(IEnumerable<Guid> entityIds)
        {
            await _priceRepository.DeleteMany(entityIds);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _priceRepository.Delete(id);
        }
    }
}

using ApiCarSale.Models;
using ApiCarSale.Models.Dtos.SellCarDto;
using ApiCarSale.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Globalization;

namespace ApiCarSale.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellCarController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SellCarService _sellCarService;
        private readonly CarService _carService;
        public SellCarController(SellCarService sellCarService, IMapper mapper, CarService carService) =>
            (_sellCarService, _mapper, _carService) = (sellCarService, mapper, carService);

        [HttpGet]
        public async Task<List<SellCar>> GetSellCars()
        {
            return await _sellCarService.GetAsync();
        }
        
        [HttpPost]
        [Route("/api/SellCar/Calculator")]
        public async Task<double> PostSaleCalculator(SearchSellCarDto searchDto)
        {
            var initialDate = searchDto.InitialDate;
            var finalDate = searchDto.FinalDate;
            var filter = Builders<SellCar>.Filter.Gte(x => x.DataVenda, searchDto.InitialDate) &
                Builders<SellCar>.Filter.Lte(x => x.DataVenda, searchDto.FinalDate);
            var sellCars = await _sellCarService.GetByDateAsync(initialDate, finalDate);

            double totalSalesValue = 0;
            foreach (var sellCar in sellCars)
            {
                totalSalesValue += sellCar.ValorVenda;
            }

            return totalSalesValue;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<SellCar>> GetSellCarId(string id)
        {
            var sellCar = await _sellCarService.GetAsync(id);

            if (sellCar == null)
            {
                return NotFound();
            }
            return sellCar;
        }
        [HttpPost]
        public async Task<IActionResult> PostSellCar([FromBody] CreateSellCarDto sellCarDto)
        {
            var sellCar = _mapper.Map<SellCar>(sellCarDto);
            var car = await _carService.GetAsync(sellCar.CarroId);
            if (car != null)
            {
                if (car.CarroVendido == true)
                {
                    return BadRequest("Este Veiculo foi vendido!");
                }
                sellCar.Carro = car;
                car.CarroVendido = true;
                await _carService.UpdateAsync(sellCar.CarroId, car);
                await _sellCarService.CreateAsync(sellCar);
                return Ok(sellCar);
            }
            return NotFound();

        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateSellCar(string id, [FromBody] UpdateSellCarDto sellCarDto)
        {
            var sellcar = await _sellCarService.GetAsync(id);
            if (sellcar == null)
            {
                return NotFound();
            }
            _mapper.Map(sellCarDto, sellcar);
            await _sellCarService.UpdateAsync(id, sellcar);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteSellCar(string id)
        {
            var sellCar = await _sellCarService.GetAsync(id);

            if (sellCar == null)
            {
                return NotFound();
            }
            var car = await _carService.GetAsync(sellCar.CarroId);
            sellCar.Carro = car;
            car.CarroVendido = false;
            await _carService.UpdateAsync(sellCar.CarroId, car);
            await _sellCarService.DeleteAsync(id);

            return NoContent();
        }
    }
}

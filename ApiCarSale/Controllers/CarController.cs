using ApiCarSale.Models.Dtos.CarDto;
using ApiCarSale.Models;
using ApiCarSale.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ApiCarSale.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CarService _carService;
        public CarController(CarService carService, IMapper mapper) =>
        (_carService, _mapper) = (carService, mapper);


        [HttpGet]
        public async Task<List<Car>> GetCars([FromQuery] bool? carSell, [FromQuery] int? carYear,[FromQuery] string? carModel, 
            [FromQuery] double? priceGreaterThan, [FromQuery] double? priceLessThan, [FromQuery] DateTime? registerGreatherThan, [FromQuery] DateTime? registerLessThan)
        {
            var filterList = new List<FilterDefinition<Car>>();

            if (carSell != null)
            {
                if (carSell == true)
                {
                    var sellFilter = Builders<Car>.Filter.Eq(c => c.CarroVendido, carSell);
                    filterList.Add(sellFilter);
                }
                else
                {
                    var sellFilter = Builders<Car>.Filter.Eq(c => c.CarroVendido, carSell);
                    filterList.Add(sellFilter);
                }
            }

            if(carYear != null)
            {   
                var yearFilter = Builders<Car>.Filter.Eq(c => c.Ano, carYear);
                filterList.Add(yearFilter);
            }

            if(carModel != null)
            {
                var modelFilter = Builders<Car>.Filter.Eq(c => c.Modelo, carModel);
                filterList.Add(modelFilter);
            }

            if(priceGreaterThan != null)
            {
                var priceGreaterFilter = Builders<Car>.Filter.Gt(c => c.Preco, priceGreaterThan);
                filterList.Add(priceGreaterFilter);
               
            }

            if(priceLessThan != null)
            {
                var priceLessfilter = Builders<Car>.Filter.Lt(c => c.Preco, priceLessThan);
                filterList.Add(priceLessfilter);
            }

            if (registerGreatherThan != null)
            {
                var registerGreatherfilter = Builders<Car>.Filter.Gt(c => c.DataCadastro, registerGreatherThan);
                filterList.Add(registerGreatherfilter);

            }

            if (registerLessThan != null)
            {
                var registerLessfilter = Builders<Car>.Filter.Lt(c => c.DataCadastro, registerLessThan);
                filterList.Add(registerLessfilter);

            }

            if (filterList.Count == 0)
            {
                var carros = await _carService.GetAsync();
                return carros;
            }

            var combinedFilter = Builders<Car>.Filter.And(filterList);
            var cars = await _carService.GetAsync(combinedFilter);

            return cars;

        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Car>> GetCarId(string id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return car;
        }

        [HttpPost]
        public async Task<IActionResult> PostCar([FromBody] CreateCarDto carDto)
        {
            var car = _mapper.Map<Car>(carDto);

            await _carService.CreateAsync(car);

            return CreatedAtAction(nameof(GetCars), new { id = car._id }, car);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCar(string id,[FromBody] UpdateCarDto updatedCar)
        {

            var car = await _carService.GetAsync(id);
            if(car == null)
            {
                return NotFound();
            }
            _mapper.Map(updatedCar, car);

            await _carService.UpdateAsync(id,car);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult>DeleteCar(string id)
        {
            var car = await _carService.GetAsync(id);

            if(car == null)
            {
                return NotFound();
            }
            await _carService.DeleteAsync(id);

            return NoContent();
        }
    }
}

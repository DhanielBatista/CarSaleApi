using ApiCarSale.Models;
using ApiCarSale.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCarSale.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;
        public CarController(CarService carService) =>
            _carService = carService;

        [HttpGet]
        public async Task<List<Car>> GetCars() =>
            await _carService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Car>> GetCarId(string id)
        {
            var car = await _carService.GetAsync(id);
            if(car == null)
            {
                return NotFound();
            }
            return car;
        }

        [HttpPost]
        public async Task<IActionResult> PostCar(Car car)
        {
            await _carService.CreateAsync(car);

            return CreatedAtAction(nameof(GetCars), new { id = car.Id }, car);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateCar(string id, Car updatedCar)
        {
            var car = await _carService.GetAsync(id);
            if(car == null)
            {
                return NotFound();
            }
            await _carService.UpdateAsync(id, updatedCar);

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

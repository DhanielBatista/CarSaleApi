﻿using ApiCarSale.Models.Dtos;
using ApiCarSale.Models;
using ApiCarSale.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<List<Car>> GetCars() =>
            await _carService.GetAsync();

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

            return CreatedAtAction(nameof(GetCars), new { id = car.Id }, car);
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

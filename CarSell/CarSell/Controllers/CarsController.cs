using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSell.DAL.DBO;
using CarSell.DAL.Parameteres;
using CarSell.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarSell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IRepository<Car> _carRepo;

        public CarsController(IRepository<Car> carRepo)
        {
            _carRepo = carRepo;
        }

        public async Task<IActionResult> Get([FromQuery] ModelParameters modelParameters)
        {
            var cars = await _carRepo.GetAllAsync(modelParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(cars.PageParam));
            return Ok(cars);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Car car)
        {
            if (car == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            await _carRepo.CreateAsync(car);
            return Created("", car);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var car = await _carRepo.GetByIdAsync(id);
            return Ok(car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Car car)
        {
            var carupd = await _carRepo.GetByIdAsync(id);
            if(carupd == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object");
            }

            await _carRepo.UpdateAsync(car, carupd);
            
         
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _carRepo.DeleteAsync(id);

            return NoContent();
        }

    }
}

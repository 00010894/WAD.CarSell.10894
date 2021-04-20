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
    public class ManufacturersController : ControllerBase
    {
        private readonly IRepository<Manufacturer> _manufacturerRepo;

        public ManufacturersController(IRepository<Manufacturer> manufacturerRepo)
        {
            _manufacturerRepo = manufacturerRepo;
        }

        public async Task<IActionResult> Get([FromQuery] ModelParameters modelParameters)
        {
            var manufacturers = await _manufacturerRepo.GetAllAsync(modelParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(manufacturers.PageParam));
            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var manufacturers = await _manufacturerRepo.GetByIdAsync(id);
            return Ok(manufacturers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Manufacturer manufacturers)
        {
            if (manufacturers == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            await _manufacturerRepo.CreateAsync(manufacturers);
            return Created("", manufacturers);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _manufacturerRepo.DeleteAsync(id);

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Manufacturer manufacturer)
        {
            var manufacturerUpd = await _manufacturerRepo.GetByIdAsync(id);
            if (manufacturerUpd == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object");
            }

            await _manufacturerRepo.UpdateAsync(manufacturer, manufacturerUpd);


            return NoContent();
        }
    }
}

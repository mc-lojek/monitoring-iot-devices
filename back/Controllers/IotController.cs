using System.Collections.Generic;
using System.Threading.Tasks;
using dot.Models;
using dot.Services;
using Microsoft.AspNetCore.Mvc;

namespace dot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IotController : ControllerBase
    {
        private readonly IotService _iotService;

        public IotController(IotService iotService) =>
            _iotService = iotService;

        [HttpGet]
        public async Task<List<Measurement>> Get() =>
            await _iotService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Measurement>> Get(string id)
        {
            var book = await _iotService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Measurement newMeasurement)
        {
            await _iotService.CreateAsync(newMeasurement);

            return CreatedAtAction(nameof(Get), new { id = newMeasurement.Id }, newMeasurement);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Measurement updatedMeasurement)
        {
            var measurement = await _iotService.GetAsync(id);

            if (measurement is null)
            {
                return NotFound();
            }

            updatedMeasurement.Id = measurement.Id;

            await _iotService.UpdateAsync(id, updatedMeasurement);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var measurement = await _iotService.GetAsync(id);

            if (measurement is null)
            {
                return NotFound();
            }

            await _iotService.RemoveAsync(id);

            return NoContent();
        }
    }
}
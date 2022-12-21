using System.Collections.Generic;
using System.Threading.Tasks;
using dot.Models;
using dot.Services;
using Microsoft.AspNetCore.Mvc;

namespace dot.Controllers
{
    [ApiController]
    [Route("api/measurement")]
    public class IotController : ControllerBase
    {
        private readonly IotService _iotService;

        public IotController(IotService iotService) =>
            _iotService = iotService;

        [HttpGet]
        public async Task<List<Measurement>> Get([FromQuery] QueryParameters query) =>
            await _iotService.GetAsync(query);

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Measurement>> Get(string id)
        {
            var measurement = await _iotService.GetAsync(id);

            if (measurement is null)
            {
                return NotFound();
            }

            return measurement;
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

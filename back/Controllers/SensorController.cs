using System.Collections.Generic;
using System.Threading.Tasks;
using dot.Models;
using dot.Services;
using Microsoft.AspNetCore.Mvc;

namespace dot.Controllers
{
    [ApiController]
    [Route("api/sensor")]
    public class SensorController : ControllerBase
    {
        private readonly SensorService _sensorService;

        public SensorController(SensorService service) =>
            _sensorService = service;

        [HttpGet]
        public async Task<List<Sensor>> Get() =>
            await _sensorService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Sensor>> Get(string id)
        {
            var measurement = await _sensorService.GetAsync(id);

            if (measurement is null)
            {
                return NotFound();
            }

            return measurement;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Sensor newSensor)
        {
            await _sensorService.CreateAsync(newSensor);

            return CreatedAtAction(nameof(Get), new { id = newSensor.Id }, newSensor);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Sensor updatedSensor)
        {
            var sensor = await _sensorService.GetAsync(id);

            if (sensor is null)
            {
                return NotFound();
            }

            updatedSensor.Id = sensor.Id;

            await _sensorService.UpdateAsync(id, updatedSensor);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var measurement = await _sensorService.GetAsync(id);

            if (measurement is null)
            {
                return NotFound();
            }

            await _sensorService.RemoveAsync(id);

            return NoContent();
        }
    }
}

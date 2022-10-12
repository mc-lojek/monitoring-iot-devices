using Microsoft.AspNetCore.Mvc;

namespace dot.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorController: ControllerBase
{
    [HttpGet(Name= "GetSensorData")]
    public String Get()
    {
        return "Hello world!";
    }
}
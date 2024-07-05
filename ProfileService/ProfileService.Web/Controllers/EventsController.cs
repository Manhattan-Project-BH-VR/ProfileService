using ProfileService.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProfileService.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IRabbitMQPublisher _rabbitMQPublisher;

    public EventsController(IRabbitMQPublisher rabbitMQPublisher)
    {
        _rabbitMQPublisher = rabbitMQPublisher;
    }

    [HttpGet("createDefaultEvent")]
    public IActionResult CreateDefaultEvent()
    {
        var message = "test";
        
        Console.WriteLine(message);
        
        _rabbitMQPublisher.Publish(message);
        return Ok(new { Message = "Event published to RabbitMQ", Event = message });
    }
    
    [HttpPost]
    public IActionResult CreateEvent([FromBody] string message)
    {
        _rabbitMQPublisher.Publish(message);
        return Ok(new { Message = "Event published to RabbitMQ", Event = message });
    }
}

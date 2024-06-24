using Microsoft.AspNetCore.Mvc;
using TT2.API.Dto;
using TT2.API.Interfaces;

namespace TT2.API.Controllers;

[Route("api/Events")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost]
    public async Task<ActionResult<EventDto>> SendEvent(EventDto eventDto, CancellationToken cancellationToken)
    {
        var newEvent = await _eventService.SendEventAsync(eventDto, cancellationToken);
        return Ok(newEvent);
    }

    [HttpGet]
    public async Task<ActionResult<List<KeyValuePair<DateTime, int>>>> GetEventByTimePeriod(DateTime startTime,
        DateTime endTime, CancellationToken cancellationToken)
    {
        var eventData = await _eventService.GetEventByTimePeriod(startTime, endTime, cancellationToken);
        return Ok(eventData);
    }
}
using Microsoft.EntityFrameworkCore;
using TT2.API.Dto;
using TT2.API.Interfaces;
using TT2.Engine.DbContexts;
using TT2.Engine.Entities;

namespace TT2.API.Services;

public class EventService : IEventService
{
    private readonly TestTaskV2DbContext _testTaskV2DbContext;

    public EventService(TestTaskV2DbContext testTaskV2DbContext)
    {
        _testTaskV2DbContext = testTaskV2DbContext;
    }

    public async Task<EventDto> SendEventAsync(EventDto eventDto, CancellationToken cancellationToken)
    {
        var newEvent = new Event
        {
            Name = eventDto.Name,
            Value = eventDto.Value,
            TimeStamp = eventDto.TimeStamp,
        };

        _testTaskV2DbContext.Events.Add(newEvent);
        await _testTaskV2DbContext.SaveChangesAsync(cancellationToken);
        return new EventDto
        {
            Name = newEvent.Name,
            Value = newEvent.Value,
            TimeStamp = newEvent.TimeStamp
        };
    }

    public async Task<List<KeyValuePair<DateTime, int>>> GetEventByTimePeriod(DateTime startTime, DateTime endTime,
        CancellationToken cancellationToken)
    {
        var eventData = await _testTaskV2DbContext.Events
            .Where(e => e.TimeStamp >= startTime && e.TimeStamp <= endTime)
            .GroupBy(e => new
            {
                Minute = e.TimeStamp.Minute, Hour = e.TimeStamp.Hour, Day = e.TimeStamp.Day, Month = e.TimeStamp.Month,
                Year = e.TimeStamp.Year
            })
            .Select(g => new KeyValuePair<DateTime, int>(
                new DateTime(g.Key.Year, g.Key.Month, g.Key.Day, g.Key.Hour, g.Key.Minute, 0),
                g.Sum(e => e.Value)))
            .ToListAsync(cancellationToken);

        return eventData;
    }
}
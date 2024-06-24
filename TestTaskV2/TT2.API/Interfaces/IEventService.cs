using TT2.API.Dto;

namespace TT2.API.Interfaces;

public interface IEventService
{
    Task<EventDto> SendEventAsync(EventDto eventDto, CancellationToken cancellationToken);

    Task<List<KeyValuePair<DateTime, int>>> GetEventByTimePeriod(DateTime startTime, DateTime endTime,
        CancellationToken cancellationToken);
}
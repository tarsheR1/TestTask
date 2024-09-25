namespace WebApplication1.ServerApp.Сore.Contracts
{
    public record GetEventsResponse(List<EventDto> Events);

    public record EventDto(Guid Id, string Title, string EventDateTime, string Location);

}

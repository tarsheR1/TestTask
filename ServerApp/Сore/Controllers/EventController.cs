using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication1.DataAccess;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Сore.Interfaces;
using WebApplication1.Сore.Models;
using WebApplication1.Сore.Contracts;

namespace WebApplication1.Сore.Controllers
{
    [ApiController]
    [Route("controller")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminPermission")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request, CancellationToken cancellationToken)
        {
            var (@event, error) = Event.Create(
                    Guid.NewGuid(),
                    request.Title,
                    request.DateTime,
                    request.Location
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var eventId = await _eventService.CreateEvent(@event);

            return Ok(eventId);
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdminPermission")]
        public async Task<ActionResult<List<GetEventsResponse>>> GetEvents([FromQuery] GetEventsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var events = await _eventService.GetEvents(request.Search, request.SortItem, request.SortOrder);

                Func<Event, object> selectorKey = request.SortItem?.ToLower() switch
                {
                    "title" => @event => @event.Title,
                    "location" => @event => @event.Location,
                    "eventdatetime" => @event => @event.EventDateTime,
                    _ => @event => @event.Id,
                };

                events.Sort((x, y) => request.SortOrder == "desc" ?
                Comparer<object>.Default.Compare(selectorKey.Invoke(y), selectorKey.Invoke(x)) :
                Comparer<object>.Default.Compare(selectorKey.Invoke(x), selectorKey.Invoke(y)));

                var response = events.Select(e => new EventDto(e.Id, e.Title, e.EventDateTime.ToString(), e.Location)).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut]
        [Authorize(Policy = "RequireUserPermission")]
        public async Task<ActionResult<Guid>> UpdateEvent(Guid id, [FromBody] CreateEventRequest updateRequest)
        {
            var eventId = await _eventService.UpdateEvent(id, updateRequest.Title, updateRequest.DateTime, updateRequest.Location);

            return Ok(eventId);
        }

        [HttpDelete]
        [Authorize(Policy = "RequireAdminPermission")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            return Ok(await _eventService.DeleteEvent(id));
        }

        private Expression<Func<Event, object>> GetSelectorKey(string sortItem)
        {
            return sortItem switch
            {
                "title" => @event => @event.Title,
                "location" => @event => @event.Location,
                "eventdatetime" => @event => @event.EventDateTime,
                _ => @event => @event.Id,
            };
        }
    }
}



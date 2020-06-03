namespace AmHaulage.WebApi.Controllers
{
    using System.Collections.Generic;
    using AmHaulage.Services.Contracts;
    using AmHaulage.WebApi.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("calendar")]
    public class CalendarController : ControllerBase
    {
        private IEventCreatorService eventCreatorService;
        private IEventDeleterService eventDeleterService;
        private IEventReaderService eventReaderService;
        private IEventUpdaterService eventUpdaterService;

        public CalendarController(
            IEventCreatorService eventCreatorService,
            IEventDeleterService eventDeleterService,
            IEventReaderService eventReaderService,
            IEventUpdaterService eventUpdaterService)
        {
            this.eventCreatorService = eventCreatorService;
            this.eventDeleterService = eventDeleterService;
            this.eventReaderService = eventReaderService;
            this.eventUpdaterService = eventUpdaterService;
        }

        [HttpGet("events/{year:int}/{month:int}")]
        public IEnumerable<EventResponse> GetEvents(int year, int month)
        {
            return new List<EventResponse>();
        }

        [HttpGet("events/{eventId:long}")]
        public EventResponse GetEvent(long eventId)
        {
            return new EventResponse();
        }

        [HttpPost("events")]
        public IActionResult CreateEvent(CreateEventRequest request)
        {
            return this.Accepted();
        }

        [HttpPut("events/{eventId:long}")]
        public IActionResult UpdateEvent(long eventId, [FromBody]UpdateEventRequest request)
        {
            return this.Accepted();
        }

        [HttpDelete("events/{eventId:long}")]
        public IActionResult DeleteEvent(long eventId)
        {
            return this.Accepted();
        }
    }
}

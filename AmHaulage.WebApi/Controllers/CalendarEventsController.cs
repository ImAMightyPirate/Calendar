// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.WebApi.Controllers
{
    using System.Collections.Generic;
    using AmHaulage.DomainObjects;
    using AmHaulage.Services.Contracts;
    using AmHaulage.Services.Contracts.Exceptions;
    using AmHaulage.WebApi.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/calendar/events")]
    public class CalendarEventsController : ControllerBase
    {
        private readonly IEventCreatorService eventCreatorService;
        private readonly IEventDeleterService eventDeleterService;
        private readonly IEventReaderService eventReaderService;
        private readonly IEventUpdaterService eventUpdaterService;

        public CalendarEventsController(
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

        [HttpGet("{year:int}/{month:int}")]
        public IEnumerable<EventSummaryResponse> GetEvents(int year, int month)
        {
            var domainObjects = this.eventReaderService.GetCalendarEvents(year, month);
            var responses = new List<EventSummaryResponse>();

            foreach (var obj in domainObjects)
            {
                yield return new EventSummaryResponse
                {
                    Id = obj.Id,
                    Summary = obj.Summary,
                    Location = obj.Location,
                    StartDate = obj.StartDate,
                    EndDate = obj.EndDate,
                };
            }
        }

        [HttpGet("{eventId:long}")]
        public EventDetailedResponse GetEvent(long eventId)
        {
            CalendarEventDO domainObject;

            try
            {
                domainObject = this.eventReaderService.GetCalendarEvent(eventId);
            }
            catch (RecordNotFoundException)
            {
                return null;
            }

            return new EventDetailedResponse
            {
                Id = domainObject.Id,
                Summary = domainObject.Summary,
                Location = domainObject.Location,
                StartDate = domainObject.StartDate,
                EndDate = domainObject.EndDate,
                IsDeleted = domainObject.IsDeleted,
            };
        }

        [HttpPost]
        public IActionResult CreateEvent(CreateEventRequest request)
        {
            var domainObject = new CalendarEventDO
            {
                CreateRequestId = request.RequestId,
                Summary = request.Summary,
                Location = request.Location,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            try
            {
                this.eventCreatorService.CreateCalendarEvent(domainObject);
            }
            catch (DuplicateRequestException)
            {
                return this.Conflict();
            }

            return this.Accepted();
        }

        [HttpPut("{eventId:long}")]
        public IActionResult UpdateEvent(long eventId, [FromBody]UpdateEventRequest request)
        {
            this.eventUpdaterService.UpdateCalendarEvent(
                eventId, 
                request.Summary, 
                request.Location,
                request.StartDate,
                request.EndDate);

            return this.Accepted();
        }

        [HttpDelete("{eventId:long}")]
        public IActionResult DeleteEvent(long eventId)
        {
            this.eventDeleterService.DeleteCalendarEvent(eventId);
            return this.Accepted();
        }
    }
}

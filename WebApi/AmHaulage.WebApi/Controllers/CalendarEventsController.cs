// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.WebApi.Controllers
{
    using System.Collections.Generic;
    using AmHaulage.DomainObjects;
    using AmHaulage.Services.Contracts;
    using AmHaulage.Services.Contracts.Exceptions;
    using AmHaulage.WebApi.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller that exposes functionality relating to calendar events.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/calendar/events")]
    public class CalendarEventsController : ControllerBase
    {
        private readonly IEventCreatorService eventCreatorService;
        private readonly IEventDeleterService eventDeleterService;
        private readonly IEventReaderService eventReaderService;
        private readonly IEventUpdaterService eventUpdaterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarEventsController" /> class.
        /// </summary>
        /// <param name="eventCreatorService">The event creator service.</param>
        /// <param name="eventDeleterService">The event deleter service.</param>
        /// <param name="eventReaderService">The event reader service.</param>
        /// <param name="eventUpdaterService">The event updater service.</param>
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

        /// <summary>
        /// Gets all of the calendar events that occur during the specified time period.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns>The matching calendar events.</returns>
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

        /// <summary>
        /// Gets the calendar event for the calendar event ID.
        /// </summary>
        /// <param name="calendarEventId">The calendar event ID.</param>
        /// <returns>The matching calendar event.</returns>
        [HttpGet("{calendarEventId:long}")]
        public EventDetailedResponse GetEvent(long calendarEventId)
        {
            CalendarEventDO domainObject;

            try
            {
                domainObject = this.eventReaderService.GetCalendarEvent(calendarEventId);
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

        /// <summary>
        /// Creates a new calendar event.
        /// </summary>
        /// <param name="request">The request defining the details of the new event.</param>
        /// <returns>The HTTP status code.</returns>
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

        /// <summary>
        /// Updates an existing calendar event.
        /// </summary>
        /// <param name="calendarEventId">The ID of the calendar event to be updated.</param>
        /// <param name="request">The request defining the details of the updated event.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPut("{calendarEventId:long}")]
        public IActionResult UpdateEvent(long calendarEventId, [FromBody]UpdateEventRequest request)
        {
            this.eventUpdaterService.UpdateCalendarEvent(
                calendarEventId,
                request.Summary,
                request.Location,
                request.StartDate,
                request.EndDate);

            return this.Accepted();
        }

        /// <summary>
        /// Deletes an existing calendar event.
        /// </summary>
        /// <param name="calendarEventId">The ID of the calendar event to be deleted.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpDelete("{calendarEventId:long}")]
        public IActionResult DeleteEvent(long calendarEventId)
        {
            this.eventDeleterService.DeleteCalendarEvent(calendarEventId);
            return this.Accepted();
        }
    }
}

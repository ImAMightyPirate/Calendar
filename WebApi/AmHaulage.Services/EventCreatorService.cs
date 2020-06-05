// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services
{
    using System.Linq;
    using AmHaulage.DomainObjects;
    using AmHaulage.Persistent.Contexts;
    using AmHaulage.Persistent.Entities;
    using AmHaulage.Services.Contracts;
    using AmHaulage.Services.Contracts.Exceptions;
    using EnsureThat;
    using Microsoft.Extensions.Logging;

    public class EventCreatorService : IEventCreatorService
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initialises a new instance of the <see cref="EventCreatorService" /> class.
        /// </summary>
        /// <param name="logger">The ASP.NET Core logger.</param>
        public EventCreatorService(ILogger<EventCreatorService> logger)
        {
            this.logger = logger;
        }

        public void CreateCalendarEvent(CalendarEventDO calendarEvent)
        {
            EnsureArg.IsNotNull(calendarEvent, nameof(calendarEvent));

            var entity = new CalendarEvent
            {
                CreateRequestId = calendarEvent.CreateRequestId,
                Summary = calendarEvent.Summary,
                Location = calendarEvent.Location,
                StartDate = calendarEvent.StartDate.Date,
                EndDate = calendarEvent.EndDate.Date,
                IsDeleted = false,
            };

            using (var context = new AmHaulageContext())
            {
                // No explicit requirement for duplicate request protection,
                // it is however desirable to ensure that two records are not
                // created if the API is hit twice in quick succession with the same data
                var existingRequestRecord = context.CalendarEvents
                    .Where(e => e.CreateRequestId == calendarEvent.CreateRequestId)
                    .SingleOrDefault();

                if (existingRequestRecord != null)
                {
                    this.logger.LogError($"New calendar event could not be created. Detected duplicate record for request ID '{calendarEvent.CreateRequestId}'.");
                    throw new DuplicateRequestException();
                }

                context.CalendarEvents.Add(entity);
                context.SaveChanges();
            }

            this.logger.LogInformation($"New calendar event created for request ID '{calendarEvent.CreateRequestId}'.");
        }
    }
}

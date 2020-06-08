// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services
{
    using System.Linq;
    using AmHaulage.DomainObjects;
    using AmHaulage.Persistence.Contracts;
    using AmHaulage.Persistence.Contracts.Entities;
    using AmHaulage.Services.Contracts;
    using AmHaulage.Services.Contracts.Exceptions;
    using AmHaulage.Services.Contracts.Validators;
    using EnsureThat;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Service with responsibility for creating calendar events.
    /// </summary>
    public class EventCreatorService : IEventCreatorService
    {
        private readonly ILogger logger;
        private readonly ICalendarEventValidator calendarEventValidator;
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCreatorService" /> class.
        /// </summary>
        /// <param name="logger">The ASP.NET Core logger.</param>
        /// <param name="calendarEventValidator">The calendar event validator.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public EventCreatorService(
            ILogger<EventCreatorService> logger,
            ICalendarEventValidator calendarEventValidator,
            IRepositoryFactory repositoryFactory)
        {
            this.logger = logger;
            this.calendarEventValidator = calendarEventValidator;
            this.repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Creates a new calendar event.
        /// </summary>
        /// <param name="calendarEvent">The calendar event details.</param>
        /// <exception cref="DuplicateRequestException">Exception thrown when the request has been made multiple times.</exception>
        public void CreateCalendarEvent(CalendarEventDO calendarEvent)
        {
            // Guards
            EnsureArg.IsNotNull(calendarEvent, nameof(calendarEvent));
            this.calendarEventValidator.ValidateSummary(calendarEvent.Summary);
            this.calendarEventValidator.ValidateLocation(calendarEvent.Location);
            this.calendarEventValidator.ValidateDateRange(calendarEvent.StartDate, calendarEvent.EndDate);

            var entity = new CalendarEvent
            {
                CreateRequestId = calendarEvent.CreateRequestId,
                Summary = calendarEvent.Summary,
                Location = calendarEvent.Location,
                StartDate = calendarEvent.StartDate.Date,
                EndDate = calendarEvent.EndDate.Date,
                IsDeleted = false,
            };

            using (var repo = this.repositoryFactory.Create())
            {
                // No explicit requirement for duplicate request protection,
                // it is however desirable to ensure that two records are not
                // created if the API is hit twice in quick succession with the same data
                var existingRequestRecord = repo.CalendarEvents
                    .Where(e => e.CreateRequestId == calendarEvent.CreateRequestId)
                    .SingleOrDefault();

                if (existingRequestRecord != null)
                {
                    this.logger.LogError($"New calendar event could not be created. Detected duplicate record for request ID '{calendarEvent.CreateRequestId}'.");
                    throw new DuplicateRequestException();
                }

                repo.AddCalendarEvent(entity);
                repo.SaveChanges();
            }

            this.logger.LogInformation($"New calendar event created for request ID '{calendarEvent.CreateRequestId}'.");
        }
    }
}
